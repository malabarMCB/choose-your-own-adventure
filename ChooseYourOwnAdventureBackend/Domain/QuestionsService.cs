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
            void FillCteResult(QuestionTreeNode node, QuestionEntity currentEntity, Queue<QuestionEntity> entities)
            {
                if( currentEntity.PositiveAnswerQuestionId.HasValue || currentEntity.NegativeAnswerQuestionId.HasValue)
                    node.Children = new List<QuestionTreeNode>();
                else
                {
                    return;
                }

                QuestionEntity positiveEntity = null;
                QuestionTreeNode positiveNode = null;

                QuestionEntity negativeEntity = null;
                QuestionTreeNode negativeNode = null;

                if (currentEntity.PositiveAnswerQuestionId.HasValue)
                {
                    positiveEntity = entities.Dequeue();
                    positiveNode = positiveEntity.ToQuestionTreeNode(QuestionAnswer.Positive);
                    node.Children.Add(positiveNode);
                }

                if (currentEntity.NegativeAnswerQuestionId.HasValue)
                {
                    negativeEntity = entities.Dequeue();
                    negativeNode = negativeEntity.ToQuestionTreeNode(QuestionAnswer.Negative);
                    node.Children.Add(negativeNode);
                }

                if(negativeNode != null)
                    FillCteResult(negativeNode, negativeEntity, entities);

                if(positiveEntity != null)
                    FillCteResult(positiveNode, positiveEntity, entities);
            }

            var firstQuestion = GetFirstQuestionEntityAsNoTracking();
            if (firstQuestion == null)
                return null;

            var query = _context.Questions.FromSqlRaw("exec GetQuestionsTree {0}", firstQuestion.Id).AsNoTracking();

            var entities = new Queue<QuestionEntity>(query);

            var root = entities.Dequeue();
            var result = root.ToQuestionTreeNode(null);

            FillCteResult(result, root, entities);

            return result;
        }

        public Question GetFirstQuestion()
        {
            return GetFirstQuestionEntityAsNoTracking()?.ToQuestion();
        }

        private QuestionEntity GetFirstQuestionEntityAsNoTracking()
        {
            return _context.Questions
                .AsNoTracking()
                .Where(question => !_context.Questions.Any(x => x.PositiveAnswerQuestionId == question.Id))
                .SingleOrDefault(question => !_context.Questions.Any(x => x.NegativeAnswerQuestionId == question.Id));
        }
    }
}