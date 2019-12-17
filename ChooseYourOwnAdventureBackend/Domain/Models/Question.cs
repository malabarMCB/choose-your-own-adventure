using DataAccess.Entities;

namespace Domain.Models
{
    public class Question
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public int? PositiveAnswerQuestionId { get; set; }

        public int? NegativeAnswerQuestionId { get; set; }
    }

    public static class QuestionExtensions
    {
        public static Question ToQuestion(this QuestionEntity entity)
        {
            return new Question
            {
                Id = entity.Id,
                Text = entity.Text,
                NegativeAnswerQuestionId = entity.NegativeAnswerQuestionId,
                PositiveAnswerQuestionId = entity.PositiveAnswerQuestionId
            };
        }
    }
}