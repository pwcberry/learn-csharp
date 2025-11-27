using System.CommandLine;

namespace CSharp.HelloWorld
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Command nameSubCommand = new("name", "Greet a user by their name")
            {
                TreatUnmatchedTokensAsErrors = false
            };

            Command sumSubCommand = new("sum", "Sums a list of numbers")
            {
                TreatUnmatchedTokensAsErrors = false             
            };            

            Option<string> multiplierOption = new("--multiply", "-m")
            {
                Description = "The multiplier for the sum"
            };

            Option<string> precisionOption = new("--precision", "-p")
            {
                Description = "The number of decimal places for floating-point sums"
            };

            RootCommand rootCommand = new("Hello World Application")
            {
                nameSubCommand,
                sumSubCommand,
                multiplierOption,
                precisionOption
            };

            ParseResult parseResult = rootCommand.Parse(args);
            if (parseResult.Errors.Count == 0)
            {
                if (parseResult.CommandResult.Command.Name == "sum")
                {
                    string[] numbers = [.. parseResult.UnmatchedTokens];
                    string multiplier = parseResult.GetValue(multiplierOption) ?? "1";
                    string precision = parseResult.GetValue(precisionOption) ?? "0";

                    string sumResult = SumInput.Sum(numbers, multiplier, precision);
                    Console.WriteLine($"Sum: {sumResult}");
                } else if (parseResult.CommandResult.Command.Name == "name")
                {
                    string name = string.Join(" ", parseResult.UnmatchedTokens.Take(3));
                    Console.WriteLine($"Hello, {name}!");
                }
            }
            else
            {
                foreach (var error in parseResult.Errors)
                {
                    Console.WriteLine($"Error: {error.Message}");
                }

            }
        }
    }
}