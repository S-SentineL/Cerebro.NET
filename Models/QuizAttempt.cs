using System;

namespace Cerebro.NET.Models
{
    public class QuizAttempt
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int QuizId { get; set; }
        public double Score { get; set; }
        public DateTime AttemptDate { get; set; }

        public ApplicationUser User { get; set; }
        public Quiz Quiz { get; set; }
    }
}
