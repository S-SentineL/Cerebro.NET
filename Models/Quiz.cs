using System;
using System.Collections.Generic;

namespace Cerebro.NET.Models
{
    public class Quiz
    {
        public int Id { get; set; }
        public int DocumentId { get; set; }
        public DateTime CreatedDate { get; set; }

        public Document Document { get; set; }
        public ICollection<Question> Questions { get; set; }
    }
}
