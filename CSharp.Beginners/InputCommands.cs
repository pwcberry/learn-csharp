using System.CommandLine;
using System.CommandLine.Parsing;
using System.Diagnostics;

namespace CSharp.Beginners;

public class InputCommands
{
    private readonly RootCommand _rootCommand;
    private readonly List<Option<string>> _options;
    
    public InputCommands()
    {
        Command nameSubCommand = new("name", "Greet a user by their name")
        {
            TreatUnmatchedTokensAsErrors = false
        };

        Command sumSubCommand = new("sum", "Sums a list of numbers")
        {
            TreatUnmatchedTokensAsErrors = false
        };
        
        _options = [
            new Option<string>("--multiply", "-m")
            {
                Description = "The multiplier for the sum"
            },
            new Option<string>("--precision", "-p")
            {
                Description = "The number of decimal places for floating-point sums"
            }
        ];

        _rootCommand = new("Hello World Application")
        {
            nameSubCommand,
            sumSubCommand,
            _options[0],
            _options[1],
        };
    }

    public CommandResult ParseArgs(string[] args)
    {
        ParseResult result = _rootCommand.Parse(args);
        string name = result.CommandResult.Command.Name;
        IDictionary<string, string?> options = _options.Select(option => new { Key = option.Name, Value = result.GetValue(option) })
            .ToDictionary(kv => kv.Key, kv => kv.Value);
        IList<string> unmatchedTokens = result.UnmatchedTokens.ToList();
        IList<string> errors = result.Errors.Select(e => e.Message).ToList();
        
        return new CommandResult(name, options, unmatchedTokens, errors);
    }
}

public class CommandResult(string name, IDictionary<string, string?> options, IList<string> unmatchedTokens, IList<string> errors)
{
    public string Name { get; } = name;
    
    public IDictionary<string, string?> Options { get; } = options;
    
    public IList<string> UnmatchedTokens { get; } = unmatchedTokens;
    
    public IList<string> Errors { get; } = errors;
}