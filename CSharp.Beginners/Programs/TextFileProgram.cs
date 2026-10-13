using CSharp.Beginners.Commands;

namespace CSharp.Beginners.Programs;

public class TextFileProgram: SubProgram<TextFileCommand, TextFileCommandResult>
{
    public override void Run(string[] inputArguments, TextWriter output)
    {
        var (commandResult, hasErrors) = ParseCommand(inputArguments, output);
        
        if (hasErrors)
            return;

        switch (commandResult.Mode)
        {
            case ReadMode.All:
                output.WriteLine($"Reading entire file: {commandResult.Arguments.First()}");
                break;
            case ReadMode.Line:
                output.WriteLine($"Reading line {commandResult.Arguments.First()} from file: {commandResult.Arguments.Last()}");
                break;
            case ReadMode.Lines:
                output.WriteLine($"Reading lines {commandResult.Arguments.First()} to {commandResult.Arguments.Skip(1).First()} from file: {commandResult.Arguments.Last()}");
                break;
            default:
                output.WriteLine("Unknown read mode.");
                break;
        }
    }
}