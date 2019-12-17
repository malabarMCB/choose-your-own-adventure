using System.Collections.Generic;
using DataAccess.Entities;

namespace Tests.Infrastructure
{
    public static class TestDataContainer
    {
        public static List<QuestionEntity> GetQuestionEntities()
        {
            return new List<QuestionEntity>
            {
                new QuestionEntity{ Id = 1, Text = "Do I want a doughnut?", PositiveAnswerQuestionId = 2, NegativeAnswerQuestionId = 3},
                new QuestionEntity{ Id = 2, Text = "Do I deserve it?", PositiveAnswerQuestionId = 4, NegativeAnswerQuestionId = 5},
                new QuestionEntity{ Id = 3, Text = "Maybe you want an apple?"},
                new QuestionEntity{ Id = 4, Text = "Are you sure?", PositiveAnswerQuestionId = 6, NegativeAnswerQuestionId = 7},
                new QuestionEntity{ Id = 5, Text = "Is it a good doughnut?", PositiveAnswerQuestionId = 8, NegativeAnswerQuestionId = 9},
                new QuestionEntity{ Id = 6, Text = "Are you really sure?", PositiveAnswerQuestionId = 10, NegativeAnswerQuestionId = 11},
                new QuestionEntity{ Id = 7, Text = "Do jumping jacks first."},
                new QuestionEntity{ Id = 8, Text = "What are you waiting for? Grab it now."},
                new QuestionEntity{ Id = 9, Text = "Wait `till you find a sinful, unforgettable doughnut."},
                new QuestionEntity{ Id = 10, Text = "Get it."},
                new QuestionEntity{ Id = 11, Text = "Why not to take a cake?"}
            };
        }
    }
}