using System.CommandLine;

namespace CSharp.Beginners.Commands;

public class BankingCommand : ICommand
{
    private readonly RootCommand _rootCommand;
    
    private readonly Argument<string> _transactionAmount = new ("transaction amount")
    {
        Description = "The amount of the transaction",
        Arity = ArgumentArity.ExactlyOne
    };
    
    public BankingCommand()
    {
        _rootCommand = new RootCommand("Banking Command")
        {
            new Command("deposit", "Deposits the provided amount into the account")
            {
                _transactionAmount
            },
            new Command("withdraw", "Withdraws the provided amount from the account")
            {
                _transactionAmount
            },
            new Command("balance", "Displays the current account balance"),
            new Command("history", "Displays the transaction history")
        };
    }

    public ICommandResult Parse(string[] args)
    {
        // Parse once and read everything from the single ParseResult.
        var parseResult = _rootCommand.Parse(args);

        return new BankingCommandResult(
            parseResult.CommandResult.Command.Name,
            new Dictionary<string, string?>(),
            parseResult.UnmatchedTokens,
            parseResult.Errors.Select(e => e.Message),
            // GetValue throws when a required argument is missing, so only read it from a valid parse.
            parseResult.Errors.Count == 0 ? parseResult.GetValue(_transactionAmount) : null
        );
    }
}

public record BankingCommandResult(
    string Name, 
    IDictionary<string, string?> Options,
    IEnumerable<string> UnmatchedTokens, 
    IEnumerable<string> Errors, 
    string? TransactionAmount
) : ICommandResult;