using System.CommandLine;

namespace CSharp.Beginners.Commands;

public class CollectionsCommand
{
    private readonly RootCommand _rootCommand;
    
    private readonly Argument<string[]> _itemsArgument = new("items")
    {
        Description = "The items to use for the collection command",
        Arity = ArgumentArity.OneOrMore
    };
    
    public CollectionsCommand()
    {
        _rootCommand = new RootCommand("Collections Program")
        {
            new Command("hello", "Say hello to a list of names")
            {
                _itemsArgument
            },
            new Command("sort", "Sorts the provided items")
            {
                _itemsArgument
            },
            new Command("fibonacci", "Generates a Fibonacci sequence up to the provided number")
            {
                _itemsArgument
            }
        };
    }
    
    public ICommandResult Parse(string[] args)
    {
        // Parse once and read everything from the single ParseResult.
        ParseResult parseResult = _rootCommand.Parse(args);

        return new CollectionsCommandResult(
            parseResult.CommandResult.Command.Name,
            new Dictionary<string, string?>(),
            parseResult.UnmatchedTokens,
            parseResult.Errors.Select(e => e.Message),
            parseResult.GetValue(_itemsArgument) ?? Enumerable.Empty<string>()
        );
    }
}

public record CollectionsCommandResult(
    string Name, 
    IDictionary<string, string?> Options,
    IEnumerable<string> UnmatchedTokens, 
    IEnumerable<string> Errors, 
    IEnumerable<string> Items
) : ICommandResult;