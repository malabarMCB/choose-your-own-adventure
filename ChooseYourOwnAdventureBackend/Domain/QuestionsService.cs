using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess;
using DataAccess.Entities;
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
            var questionEntity = _context.Questions.AsNoTracking().SingleOrDefault(x => x.Id == id);

            return questionEntity?.ToQuestion();
        }

        public QuestionTreeNode GetQuestionsTree()
        {
            QuestionTreeNode ConvertToQuestionTreeNode(QuestionEntity entity, QuestionAnswer? type)
            {
                return new QuestionTreeNode
                {
                    Id = entity.Id,
                    Text = entity.Text,
                    Type = type,
                };
            }

            void FillTree(QuestionTreeNode node, List<QuestionEntity> questions)
            {
                var question = questions.FirstOrDefault(x => x.Id == node.Id);
                if(question == null)
                    return;
                questions.Remove(question);


                if (question.PositiveAnswerQuestion != null && question.NegativeAnswerQuestion != null)
                {
                    node.Children = new List<QuestionTreeNode>();

                    var positiveAnswer = ConvertToQuestionTreeNode(question.PositiveAnswerQuestion, QuestionAnswer.Positive);
                    FillTree(positiveAnswer, questions);
                    node.Children.Add(positiveAnswer);

                    var negativeAnswer = ConvertToQuestionTreeNode(question.NegativeAnswerQuestion, QuestionAnswer.Negative);
                    FillTree(negativeAnswer, questions);
                    node.Children.Add(negativeAnswer);
                }
            }

            var query = from questions in _context.Questions
                join positiveAnswers in _context.Questions
                    on questions.PositiveAnswerQuestionId equals positiveAnswers.Id
                join negativeAnswers in _context.Questions
                    on questions.NegativeAnswerQuestionId equals negativeAnswers.Id
                select new QuestionEntity
                {
                    Id = questions.Id,
                    PositiveAnswerQuestionId = questions.PositiveAnswerQuestionId,
                    NegativeAnswerQuestionId = questions.NegativeAnswerQuestionId,
                    Text = questions.Text,
                    PositiveAnswerQuestion = positiveAnswers,
                    NegativeAnswerQuestion = negativeAnswers
                };

            var joinedQuestions = query.AsNoTracking().ToList();

            if (joinedQuestions.Count == 0)
                return null;

            var firstQuestion = joinedQuestions.First();
            var result = ConvertToQuestionTreeNode(firstQuestion, null);

            FillTree(result, joinedQuestions);

            return result;
        }

        public Question GetFirstQuestion()
        {
            return _context.Questions
                .AsNoTracking()
                .Where(question => !_context.Questions.Any(x => x.PositiveAnswerQuestionId == question.Id))
                .SingleOrDefault(question => !_context.Questions.Any(x => x.NegativeAnswerQuestionId == question.Id))
                ?.ToQuestion();
        }
    }
}