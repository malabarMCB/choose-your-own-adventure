using System.Collections.Generic;
using DataAccess.Entities;
using Domain;
using Domain.Models;
using FluentAssertions;
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
            var context = TestDbContextCreator.Create(nameof(ShouldReturnNullIfQuestionNotFound));
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

            var context = TestDbContextCreator.Create(nameof(ShouldReturnQuestion));
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
            var context = TestDbContextCreator.Create(nameof(ShouldReturnNullIfTreeIsEmpty));
            var sut = new QuestionsService(context);

            //act
            var actual = sut.GetQuestionsTree();

            //expected
            actual.Should().BeNull();
        }

        [Fact]
        public void ShouldReturnTree()
        {
            //arrange
            var context = TestDbContextCreator.Create(nameof(ShouldReturnTree));
            var questions = TestDataContainer.GetQuestionEntities();
            context.Questions.AddRange(questions);
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

        [Fact]
        public void ShouldReturnNullIfFirstQuestionIsMissing()
        {
            //arrange
            var context = TestDbContextCreator.Create(nameof(ShouldReturnNullIfFirstQuestionIsMissing));
            var sut = new QuestionsService(context);

            //act
            var actual = sut.GetFirstQuestion();

            //arrange
            actual.Should().BeNull();
        }

        [Fact]
        public void ShouldReturnFirstQuestion()
        {
            //arrange
            var context = TestDbContextCreator.Create(nameof(ShouldReturnFirstQuestion));
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

            //arrange
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
