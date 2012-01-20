using System.Collections.Generic;

namespace SimpleRulesEngine
{
    public interface IRuleEvaluator
    {
        string PrintLine(int subject, IEnumerable<IRule> rules);
    }
}