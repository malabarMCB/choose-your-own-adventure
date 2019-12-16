using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class QuestionEntity
    {
        public int Id { get; set; }

        [MaxLength(256)]
        public string Text { get; set; }

        public int? PositiveAnswerQuestionId { get; set; }

        public QuestionEntity PositiveAnswerQuestion { get; set; }

        public int? NegativeAnswerQuestionId { get; set; }

        public QuestionEntity NegativeAnswerQuestion { get; set; }

        public ICollection<QuestionEntity> InverseNavigationPositiveAnswerQuestion { get; set; }

        public ICollection<QuestionEntity> InverseNavigationNegativeAnswerQuestion { get; set; }
    }
}