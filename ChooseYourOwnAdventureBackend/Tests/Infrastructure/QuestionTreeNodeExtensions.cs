using System.Linq;
using Domain.Models;
using FluentAssertions;

namespace Tests.Infrastructure
{
    public static class QuestionTreeNodeExtensions
    {
        public static void ShouldBeEquivalentTo(this QuestionTreeNode actual, QuestionTreeNode expected)
        {
            if (actual == null)
            {
                expected.Should().BeNull();
                return;
            }

            actual.Id.Should().Be(expected.Id);
            actual.Text.Should().Be(expected.Text);
            actual.Type.Should().Be(expected.Type);

            if (actual.Children == null)
            {
                expected.Children.Should().BeNull();
                return;
            }

            actual.Children.Count.Should().Be(expected.Children.Count);

            actual.Children.FirstOrDefault(x => x.Type == QuestionAnswer.Positive).ShouldBeEquivalentTo(expected.Children.FirstOrDefault(x => x.Type == QuestionAnswer.Positive));
            actual.Children.FirstOrDefault(x => x.Type == QuestionAnswer.Negative).ShouldBeEquivalentTo(expected.Children.FirstOrDefault(x => x.Type == QuestionAnswer.Negative));
        }
    }
}