using CSharp.Beginners.Commands;

namespace CSharp.Beginners.Programs;

public abstract class SubProgram<TCommand, TCommandResult>
        where TCommand: ICommand, new()
        where TCommandResult: ICommandResult
{
    protected Tuple<TCommandResult, bool> ParseCommand(string[] inputArguments, TextWriter output)
    {
        var commandParser = new TCommand();
        var commandResult = (TCommandResult) commandParser.Parse(inputArguments);

        return CheckForErrors(commandResult, output) 
            ? new Tuple<TCommandResult, bool>(commandResult, true) 
            : new Tuple<TCommandResult, bool>(commandResult, false);
    }
    
    private static bool CheckForErrors(TCommandResult commandResult, TextWriter output)
    {
        if (!commandResult.Errors.Any()) 
            return false;
        
        foreach (var error in commandResult.Errors)
        {
            output.WriteLine($"Error: {error}");
        }
            
        return true;
    }
    
    public abstract void Run(string[] inputArguments, TextWriter output);
}