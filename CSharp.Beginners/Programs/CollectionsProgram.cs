using CSharp.Beginners.Commands;

namespace CSharp.Beginners.Programs;

public class CollectionsProgram : SubProgram
{
    public override void Run(string[] inputArguments, TextWriter output)
    {
        var commandParser = new CollectionsCommand();
        var commandResult = commandParser.Parse(inputArguments) switch
        {
            CollectionsCommandResult collectionsResult => collectionsResult,
            _ => null
        };

        if (commandResult is null)
        {
            output.WriteLine("Error: No valid command was provided. Please provide a valid subcommand.");
            return;
        }

        if (CheckForErrors(commandResult, output))
        {
            return;
        }

        switch (commandResult.Name.ToLower())
        {
            case "hello":
                foreach (var name in commandResult.Items)
                {
                    output.WriteLine("Hello, {0}!", name);
                }

                break;
            case "sort":
                var sortedItems = commandResult.Items.OrderBy(item => item, new MyStringComparer());
                output.WriteLine("Sorted items:");
                foreach (var item in sortedItems)
                {
                    output.WriteLine(item);
                }

                break;
            case "fibonacci":
                if (commandResult.Items.Count() != 1 || !int.TryParse(commandResult.Items.First(), out var max))
                {
                    output.WriteLine("Error: Please provide a single integer for the Fibonacci sequence.");
                    return;
                }

                var fibonacciSequence = GetFibonacciRange(max);
                output.WriteLine("Fibonacci sequence up to {0}:", max);
                output.WriteLine(string.Join(", ", fibonacciSequence));
                break;
            default:
                output.WriteLine("Error: Unknown command '{0}'.", commandResult.Name);
                break;
        }
    }

    private static List<int> GetFibonacciRange(int maximum)
    {
        List<int> numbers = [1, 1];

        var maximumReached = false;
        do
        {
            var nextNumber = numbers[^2] + numbers[^1];
            if (nextNumber > maximum)
            {
                maximumReached = true;
            }
            else
            {
                numbers.Add(nextNumber);
            }
        } while (!maximumReached);
        
        return numbers;
    }
}

/// <summary>
/// Ensures strings are equal or comparable no matter their case.
/// </summary>
public class MyStringComparer : IEqualityComparer<string>, IComparer<string>
{
    public int Compare(string? x, string? y)
    {
        return x?.ToUpper()?.CompareTo(y?.ToUpper()) ?? 0;
    }

    public bool Equals(string? x, string? y)
    {
        return string.Equals(x?.ToUpper(), y?.ToUpper());
    }

    public int GetHashCode(string obj)
    {
        return obj.GetHashCode();
    }
}