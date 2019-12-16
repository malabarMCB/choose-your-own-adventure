using System;
using System.Linq;
using DataAccess;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain
{
    public class QuestionsService: IQuestionsService
    {
        private readonly QuestionsDbContext _context;

        public QuestionsService(QuestionsDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Question GetQuestion(int id)
        {
            var question = _context.Questions.AsNoTracking().SingleOrDefault(x => x.Id == id);

            return question == null ? null : new Question
            {
                Id = question.Id,
                Text = question.Text,
                NegativeAnswerQuestionId = question.NegativeAnswerQuestionId,
                PositiveAnswerQuestionId = question.PositiveAnswerQuestionId
            };
        }

        public QuestionTreeNode GetQuestionsTree()
        {
            throw new System.NotImplementedException();
        }
    }
}