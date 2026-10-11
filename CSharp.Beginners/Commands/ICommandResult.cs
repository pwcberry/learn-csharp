namespace CSharp.Beginners.Commands;

public interface ICommandResult 
{
    public string Name { get; }
    public IDictionary<string, string?> Options { get; }
    public IEnumerable<string> UnmatchedTokens { get; }
    public IEnumerable<string> Errors { get; }
}
