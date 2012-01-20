using FluentAssertions;
using NUnit.Framework;
using SimpleRulesEngine.Rules;

namespace Specs
{
    [TestFixture]
    public class When_using_a_DivisibleByReturn_rule
    {
        #region Setup/Teardown

        [SetUp]
        public void Setup()
        {
            _divisibleRule = new DivisibleByRule(3, "blah");
        }

        #endregion

        private DivisibleByRule _divisibleRule;

        [Test]
        public void Should_evaluate_false_if_subject_isnt_divisible()
        {
            _divisibleRule.Matches(2).Should().BeFalse();
            _divisibleRule.Matches(5).Should().BeFalse();
            _divisibleRule.Matches(10).Should().BeFalse();
            _divisibleRule.Matches(52).Should().BeFalse();
        }

        [Test]
        public void Should_evaluate_true_if_subject_is_a_multiple()
        {
            _divisibleRule.Matches(3).Should().BeTrue();
            _divisibleRule.Matches(6).Should().BeTrue();
            _divisibleRule.Matches(15).Should().BeTrue();
            _divisibleRule.Matches(18).Should().BeTrue();
        }
    }
}