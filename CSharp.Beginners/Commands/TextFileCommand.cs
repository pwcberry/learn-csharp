using System.CommandLine;

namespace CSharp.Beginners.Commands;

public class TextFileCommand : ICommand
{
    private readonly RootCommand _rootCommand;

    private readonly Option<string> _readModeOption = new("--mode", "-m")
    {
        Description = "Specifies the mode of reading: 'all', 'line', or 'lines'",
        Recursive = true
    };
    
    private readonly Argument<string[]> _readArguments = new("arguments")
    {
        Description = "Additional arguments based on the mode",
        Arity = ArgumentArity.ZeroOrMore
    };

    public TextFileCommand()
    {
        _rootCommand = new RootCommand("Text File Command")
        {
            new Command("read", "Reads the contents of a text file")
            {
                _readModeOption,
                _readArguments
            }
        };
    }

    public ICommandResult Parse(string[] args)
    {
        var parseResult = _rootCommand.Parse(args);
        var mode = ParseMode(parseResult.GetValue<string>("--mode"));
        var arguments = parseResult.GetValue(_readArguments) ?? [];
        var errors = parseResult.Errors.Select(e => e.Message).ToList();

        if (parseResult.CommandResult.Command.Name == "read" && mode == ReadMode.Error)
        {
            errors.Add("The correct read mode is required. Please specify 'all', 'line', or 'lines'.");
        }
        else if (mode != ReadMode.Error && arguments.Length != ExpectedArgumentCount(mode))
        {
            errors.Add($"The '{mode.ToString().ToLowerInvariant()}' mode requires {ExpectedArgumentCount(mode)} argument(s).");
        }

        return new TextFileCommandResult(
            parseResult.CommandResult.Command.Name,
            new Dictionary<string, string?>(),
            parseResult.UnmatchedTokens,
            errors,
            mode,
            arguments
        );
    }

    private static ReadMode ParseMode(string? modeValue) =>
        Enum.TryParse<ReadMode>(modeValue, true, out var mode) && mode is not ReadMode.Error
            ? mode
            : ReadMode.Error;

    private static int ExpectedArgumentCount(ReadMode mode) => mode switch
    {
        ReadMode.All => 1,
        ReadMode.Line => 2,
        ReadMode.Lines => 3,
        _ => 0
    };
}

public record TextFileCommandResult(
    string Name,
    IDictionary<string, string?> Options,
    IEnumerable<string> UnmatchedTokens,
    IEnumerable<string> Errors,
    ReadMode Mode,
    IEnumerable<string> Arguments
) : ICommandResult;

public enum ReadMode
{
    All,
    Line,
    Lines,
    Error
}