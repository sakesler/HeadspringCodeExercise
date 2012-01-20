using System.Collections.Generic;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SimpleRulesEngine;
using SimpleRulesEngine.Evaluators;

namespace Specs
{
    [TestFixture]
    public class When_subject_checked_against_MatchingRulesAggregator
    {
        private readonly MatchingRulesAggregator _matchingRulesAggregator = new MatchingRulesAggregator();

        private static Mock<IRule> MatchingRuleReturningThis(string alternatevalue)
        {
            var passingRule = new Mock<IRule>();

            passingRule.Setup(x => x.Matches(It.IsAny<int>()))
                .Returns(true);
            passingRule.Setup(x => x.Alternate)
                .Returns(alternatevalue);

            return passingRule;
        }

        private static Mock<IRule> FailingMatchRule()
        {
            var passingRule = new Mock<IRule>();

            passingRule.Setup(x => x.Matches(It.IsAny<int>()))
                .Returns(false);

            return passingRule;
        }

        [Test]
        public void Should_print_alternate_value_if_matches()
        {
            const string alternatevalue = "AlternateValue";
            Mock<IRule> mockRule = MatchingRuleReturningThis(alternatevalue);

            _matchingRulesAggregator.StringToWrite(new[] {mockRule.Object}, 0)
                .Should().Be(alternatevalue);
        }

        [Test]
        public void Should_print_concatenation_of_multiple_matches()
        {
            const string alternatevalue1 = "AlternateValue1";
            const string alternatevalue2 = "AlternateValue2";
            const string alternatevalue3 = "AlternateValue3";
            Mock<IRule> matchingRule1 = MatchingRuleReturningThis(alternatevalue1);
            Mock<IRule> matchingRule2 = MatchingRuleReturningThis(alternatevalue2);
            Mock<IRule> matchingRule3 = MatchingRuleReturningThis(alternatevalue3);
            Mock<IRule> failingRule = FailingMatchRule();

            var rules = new List<IRule>
                            {
                                matchingRule1.Object,
                                matchingRule2.Object,
                                matchingRule3.Object,
                                failingRule.Object
                            };

            _matchingRulesAggregator.StringToWrite(rules, 0)
                .Should().Be(alternatevalue1 + alternatevalue2 + alternatevalue3);
        }

        [Test]
        public void Should_print_number_if_doesnt_match()
        {
            Mock<IRule> mockRule = FailingMatchRule();

            _matchingRulesAggregator.StringToWrite(new[] {mockRule.Object}, 0)
                .Should().Be("0");
        }
    }
}