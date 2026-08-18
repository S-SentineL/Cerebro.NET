using Cerebro.NET.Data;
using Cerebro.NET.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UglyToad.PdfPig;

namespace Cerebro.NET.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/documents")]
    public class DocumentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DocumentController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            if (file.Length > 5 * 1024 * 1024)
                return BadRequest("File size exceeds 5MB limit.");

            var ext = Path.GetExtension(file.FileName).ToLower();
            if (ext != ".txt" && ext != ".pdf")
                return BadRequest("Invalid file type. Only .txt and .pdf are allowed.");

            string extractedText = "";

            if (ext == ".txt")
            {
                using var reader = new StreamReader(file.OpenReadStream());
                extractedText = await reader.ReadToEndAsync();
            }
            else if (ext == ".pdf")
            {
                using var stream = file.OpenReadStream();
                using var pdfDocument = PdfDocument.Open(stream);
                foreach (var page in pdfDocument.GetPages())
                {
                    extractedText += page.Text + " ";
                }
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var document = new Document
            {
                UserId = userId ?? string.Empty,
                FileName = file.FileName,
                UploadDate = DateTime.UtcNow,
                ExtractedText = extractedText
            };

            _context.Documents.Add(document);
            await _context.SaveChangesAsync();

            return Ok(new { Status = "Success", Message = "Document uploaded successfully.", DocumentId = document.Id });
        }
    }
}
