using System.Collections.Generic;
using DataAccess.Entities;

namespace Domain.Models
{
    public class QuestionTreeNode
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public QuestionAnswer? Type { get; set; }

        public List<QuestionTreeNode> Children { get; set; }
    }

    public static class QuestionTreeNodeExtensions
    {
        public static QuestionTreeNode ToQuestionTreeNode(this QuestionEntity entity, QuestionAnswer? type)
        {
            return new QuestionTreeNode
            {
                Id = entity.Id,
                Text = entity.Text,
                Type = type,
            };
        }
    }
}