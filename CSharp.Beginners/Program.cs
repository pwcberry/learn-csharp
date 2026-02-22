using CSharp.Beginners;

var commandLine = new InputCommands();
CommandResult command = commandLine.ParseArgs(args);

if (command.Errors.Count == 0)
{
    if (command.Name == "sum")
    {
        string[] numbers = command.UnmatchedTokens.ToArray();
        string multiplier = command.Options["--multiply"] ?? "1";
        string precision = command.Options["--precision"] ?? "0";
        string sumResult = SumInput.Sum(numbers, multiplier, precision);
        Console.WriteLine($"Sum: {sumResult}");
    }
    else if (command.Name == "name")
    {
        string name = string.Join(" ", command.UnmatchedTokens.Take(3));
        Console.WriteLine($"Hello, {name}!");
    }
}
else
{
    foreach (var error in command.Errors)
    {
        Console.WriteLine($"Error: {error}");
    }
}

// Collections.Basic();
//
// var set = Collections.CreateSortedSet(new string[] {
//     "Apple",
//     "alpha",
//     "gamma",
//     "zeta",
//     "Peach",
//     "group",
//     "Help",
//     "APPLE"
// });
//
// foreach(var item in set) {
//     Console.WriteLine(item);
// }

// Collections.SortAList();
// var numbers = Collections.GetFibonacciRange();
// foreach (var number in numbers)
// {
//     Console.WriteLine(number);
// }