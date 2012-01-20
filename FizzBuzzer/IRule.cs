namespace SimpleRulesEngine
{
    public interface IRule
    {
        string Alternate { get; }
        bool Matches(int subject);
    }
}