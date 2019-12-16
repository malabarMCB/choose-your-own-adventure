using System;
using Domain.Models;

namespace Domain
{
    public interface IQuestionsService
    {
        Question GetQuestion(int id);

        QuestionTreeNode GetQuestionsTree();
    }
}
