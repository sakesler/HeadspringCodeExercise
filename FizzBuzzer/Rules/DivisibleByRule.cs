namespace SimpleRulesEngine.Rules
{
    public class DivisibleByRule : IRule
    {
        private readonly string _alternateResult;
        private readonly int _divisor;

        public DivisibleByRule(int divisor, string alternateResult)
        {
            _divisor = divisor;
            _alternateResult = alternateResult;
        }

        #region IRule Members

        public string Alternate
        {
            get { return _alternateResult; }
        }

        public bool Matches(int subject)
        {
            return subject%_divisor == 0;
        }

        #endregion
    }
}