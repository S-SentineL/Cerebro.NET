namespace Cerebro.NET.Models
{
    public class Question
    {
        public int Id { get; set; }
        public int QuizId { get; set; }
        public string Text { get; set; }
        public string Options { get; set; } // JSON array of options
        public string CorrectAnswer { get; set; }

        public Quiz Quiz { get; set; }
    }
}
