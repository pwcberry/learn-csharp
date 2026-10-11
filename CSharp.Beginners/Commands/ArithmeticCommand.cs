using System.CommandLine;
using System.CommandLine.Parsing;
using System.Globalization;

namespace CSharp.Beginners.Commands;

public class ArithmeticCommand
{
    private readonly RootCommand _rootCommand;

    private readonly Option<string> _multiplyOption = new("--multiply", "-m")
    {
        Description = "The multiplier for the sum",
        Recursive = true
    };

    private readonly Option<string> _precisionOption = new("--precision", "-p")
    {
        Description = "The number of decimal places for floating-point sums",
        Recursive = true
    };

    private readonly Argument<string[]> _numbersArgument = new("numbers")
    {
        Description = "The integers or floating-point numbers to sum",
        Arity = ArgumentArity.ZeroOrMore,
        CustomParser = ParseNumbers
    };

    public ArithmeticCommand()
    {
        _rootCommand = new RootCommand("Arithmetic Program")
        {
            new Command("sum", "Sums a list of numbers")
            {
                _numbersArgument
            },
            _multiplyOption,
            _precisionOption
        };
    }

    public ICommandResult Parse(string[] args)
    {
        // Parse once and read everything from the single ParseResult.
        var parseResult = _rootCommand.Parse(args);

        var options = new Dictionary<string, string?>
        {
            [_multiplyOption.Name] = parseResult.GetValue(_multiplyOption),
            [_precisionOption.Name] = parseResult.GetValue(_precisionOption)
        };

        return new ArithmeticCommandResult(
            parseResult.CommandResult.Command.Name,
            options,
            parseResult.UnmatchedTokens,
            parseResult.Errors.Select(e => e.Message).ToList(),
            parseResult.GetValue(_numbersArgument) ?? []
        );
    }

    // Keeps only tokens that SumInput can parse: integers, or floats when they contain a '.'.
    private static string[] ParseNumbers(ArgumentResult result) =>
        result.Tokens
            .Select(token => token.Value)
            .Where(IsNumber)
            .ToArray();

    private static bool IsNumber(string value) =>
        value.Contains('.')
            ? float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out var f) && float.IsFinite(f)
            : int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out _);
}

public record ArithmeticCommandResult(string Name, IDictionary<string, string?> Options, IEnumerable<string> UnmatchedTokens, IEnumerable<string> Errors, IEnumerable<string> Numbers) : ICommandResult;