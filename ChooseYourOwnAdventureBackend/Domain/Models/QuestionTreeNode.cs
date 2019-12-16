using System.Collections.Generic;

namespace Domain.Models
{
    public class QuestionTreeNode
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public QuestionAnswer? Type { get; set; }

        public List<QuestionTreeNode> Children { get; set; }
    }
}