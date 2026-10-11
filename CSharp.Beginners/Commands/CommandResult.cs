namespace CSharp.Beginners.Commands;

public record CommandResult(string Name, IEnumerable<string> Numbers, IDictionary<string, string?> Options, IEnumerable<string> UnmatchedTokens, IEnumerable<string> Errors);
