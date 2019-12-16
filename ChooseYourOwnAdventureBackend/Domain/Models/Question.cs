namespace Domain.Models
{
    public class Question
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public int? PositiveAnswerQuestionId { get; set; }

        public int? NegativeAnswerQuestionId { get; set; }
    }
}