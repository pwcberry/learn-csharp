using System.Globalization;
using CSharp.Beginners.Commands;

namespace CSharp.Beginners.Programs;

public class ArithmeticProgram : SubProgram
{
    public override void Run(string[] inputArguments, TextWriter output)
    {
        var commandParser = new ArithmeticCommand();
        var commandResult = commandParser.Parse(inputArguments) switch
        {
            ArithmeticCommandResult arithmeticResult => arithmeticResult,
            _ => null
        };
        
        if (commandResult is null || !commandResult.Name.Equals("sum", StringComparison.OrdinalIgnoreCase))
        {
            output.WriteLine(
                "Error: The 'sum' command is required. Please provide the 'sum' subcommand followed by numbers to sum.");
            return;
        }

        if (CheckForErrors(commandResult, output))
        {
            return;
        }

        // The options are always present but hold null when not supplied on the command line.
        commandResult.Options.TryGetValue("--multiply", out var multiplier);
        commandResult.Options.TryGetValue("--precision", out var precision);
        multiplier ??= "1";
        precision ??= "0";

        output.WriteLine("Calculating sum on these numbers: {0}", string.Join(", ", commandResult.Numbers));
        output.WriteLine($"Multiplier: {multiplier}, Precision: {precision}");

        var sumResult = Sum(commandResult.Numbers.ToArray(), multiplier, precision);
        output.WriteLine("Sum: {0}", sumResult);
    }

    private static string Sum(string[] args, string multiplier, string precision)
    {
        if (args.Any(x => x.Contains('.')) || multiplier.Contains('.'))
        {
            var m = float.Parse(multiplier);
            var sum = args.Select(float.Parse).Sum() * m;
            return sum.ToString($"F{precision}", CultureInfo.InvariantCulture);
        }
        else
        {
            var m = int.Parse(multiplier);
            var sum = args.Select(int.Parse).Sum() * m;
            return sum.ToString();
        }
    }
}