using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SimpleRulesEngine.Evaluators
{
    public class MatchingRulesAggregator
    {
        public string StringToWrite(IEnumerable<IRule> rules, int subject)
        {
            List<IRule> listOfRules = rules.ToList();
            if (listOfRules.Any(r => r.Matches(subject)))
                return listOfRules.Where(r => r.Matches(subject))
                    .Select(x => x.Alternate)
                    .Aggregate((a, b) => a + b);
            return subject.ToString(CultureInfo.InvariantCulture);
        }
    }
}