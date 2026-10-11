using System.Globalization;
using CSharp.Beginners.Commands;

namespace CSharp.Beginners.Programs;

public class ArithmeticProgram : SubProgram<ArithmeticCommand, ArithmeticCommandResult>
{
    public override void Run(string[] inputArguments, TextWriter output)
    {
        var (commandResult, hasErrors) = ParseCommand(inputArguments, output);

        // If the "sum" command is not supplied, the commandResult will be null and hasErrors will be true.
        if (hasErrors)
            return;
        
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