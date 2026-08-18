using System;

namespace Cerebro.NET.Models
{
    public class Document
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string FileName { get; set; }
        public DateTime UploadDate { get; set; }
        public string ExtractedText { get; set; }
        
        public ApplicationUser User { get; set; }
    }
}
