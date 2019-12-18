using System.Collections.Generic;
using DataAccess.Entities;
using Domain;
using Domain.Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Tests.Infrastructure;
using Xunit;

namespace Tests
{
    public class QuestionsServiceTests
    {

        [Fact]
        public void ShouldReturnNullIfQuestionNotFound()
        {
            //arrange
            var context = TestDbContextCreator.CreateInMemory(nameof(ShouldReturnNullIfQuestionNotFound));
            var sut = new QuestionsService(context);
            var notExistingQuestionId = 1;

            //act
            var actual = sut.GetQuestion(notExistingQuestionId);

            //assert
            actual.Should().BeNull();
        }

        [Fact]
        public void ShouldReturnQuestion()
        {
            //arrange
            var questionEntity = new QuestionEntity
            {
                Id = 1,
                Text = "Test question"
            };

            var context = TestDbContextCreator.CreateInMemory(nameof(ShouldReturnQuestion));
            context.Questions.Add(questionEntity);
            context.SaveChanges();

            var expected = new Question
            {
                Id = questionEntity.Id,
                Text = questionEntity.Text
            };

            var sut = new QuestionsService(context);

            //act
            var actual = sut.GetQuestion(questionEntity.Id);

            //assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void ShouldReturnNullIfTreeIsEmpty()
        {
            //arrange
            var context = TestDbContextCreator.CreateSqlServer();
            var sut = new QuestionsService(context);

            using (context.Database.BeginTransaction())
            {
                context.Questions.RemoveRange(context.Questions);
                context.SaveChanges();

                //act
                var actual = sut.GetQuestionsTree();

                //assert
                actual.Should().BeNull();
            }
        }

        [Fact]
        public void ShouldReturnTree()
        {
            //arrange
            var context = TestDbContextCreator.CreateSqlServer();
            using (context.Database.BeginTransaction())
            {
                context.Questions.RemoveRange(context.Questions);
                context.SaveChanges();

                context.Database.ExecuteSqlRaw(@"set IDENTITY_INSERT [Questions] on
                insert into Questions
                    (Id, [Text], [PositiveAnswerQuestionId], [NegativeAnswerQuestionId])
                values
                    (1, 'Do I want a doughnut?', 2, 3),
                (2, 'Do I deserve it?', 4, 5),
                (3, 'Maybe you want an apple?', null, null),
                (4, 'Are you sure?', 6, 7),
                (5, 'Is it a good doughnut?', 8, 9),
                (6, 'Are you really sure?', 10, 11),
                (7, 'Do jumping jacks first.', null, null),
                (8, 'What are you waiting for? Grab it now.', null, null),
                (9, 'Wait `till you find a sinful, unforgettable doughnut.', null, null),
                (10, 'Get it.', null, null),
                (11, 'Why not to take a cake?', null, null)
                set IDENTITY_INSERT[Questions] off");

                context.SaveChanges();

                var sut = new QuestionsService(context);

                var expected = new QuestionTreeNode
                {
                    Id = 1,
                    Text = "Do I want a doughnut?",
                    Type = null,
                    Children = new List<QuestionTreeNode>
                    {
                        new QuestionTreeNode
                        {
                            Id = 2,
                            Text = "Do I deserve it?",
                            Type = QuestionAnswer.Positive,
                            Children = new List<QuestionTreeNode>
                            {
                                new QuestionTreeNode
                                {
                                    Id = 4,
                                    Text = "Are you sure?",
                                    Type = QuestionAnswer.Positive,
                                    Children = new List<QuestionTreeNode>
                                    {
                                        new QuestionTreeNode
                                        {
                                            Id = 6,
                                            Text = "Are you really sure?",
                                            Type = QuestionAnswer.Positive,
                                            Children = new List<QuestionTreeNode>
                                            {
                                                new QuestionTreeNode
                                                {
                                                    Id = 10,
                                                    Text = "Get it.",
                                                    Type = QuestionAnswer.Positive
                                                },
                                                new QuestionTreeNode
                                                {
                                                    Id = 11,
                                                    Text = "Why not to take a cake?",
                                                    Type = QuestionAnswer.Negative
                                                }
                                            }
                                        },
                                        new QuestionTreeNode
                                        {
                                            Id = 7,
                                            Text = "Do jumping jacks first.",
                                            Type = QuestionAnswer.Negative
                                        }
                                    }

                                },
                                new QuestionTreeNode
                                {
                                    Id = 5,
                                    Text = "Is it a good doughnut?",
                                    Type = QuestionAnswer.Negative,
                                    Children = new List<QuestionTreeNode>
                                    {
                                        new QuestionTreeNode
                                        {
                                            Id = 8,
                                            Text = "What are you waiting for? Grab it now.",
                                            Type = QuestionAnswer.Positive
                                        },
                                        new QuestionTreeNode
                                        {
                                            Id = 9,
                                            Text = "Wait `till you find a sinful, unforgettable doughnut.",
                                            Type = QuestionAnswer.Negative
                                        }
                                    }
                                }
                            }
                        },
                        new QuestionTreeNode
                        {
                            Id = 3,
                            Text = "Maybe you want an apple?",
                            Type = QuestionAnswer.Negative
                        }
                    }
                };

                //arrange
                var actual = sut.GetQuestionsTree();

                //assert
                actual.ShouldBeEquivalentTo(expected);
            }
        }

        [Fact]
        public void ShouldReturnNullIfFirstQuestionIsMissing()
        {
            //arrange
            var context = TestDbContextCreator.CreateInMemory(nameof(ShouldReturnNullIfFirstQuestionIsMissing));
            var sut = new QuestionsService(context);

            //act
            var actual = sut.GetFirstQuestion();

            //assert
            actual.Should().BeNull();
        }

        [Fact]
        public void ShouldReturnFirstQuestion()
        {
            //arrange
            var context = TestDbContextCreator.CreateInMemory(nameof(ShouldReturnFirstQuestion));
            var questions = TestDataContainer.GetQuestionEntities();
            context.Questions.AddRange(questions);
            context.SaveChanges();

            var sut = new QuestionsService(context);

            var expected = new Question
            {
                Id = 1,
                Text = "Do I want a doughnut?",
                PositiveAnswerQuestionId = 2,
                NegativeAnswerQuestionId = 3
            };

            //act
            var actual = sut.GetFirstQuestion();

            //assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
