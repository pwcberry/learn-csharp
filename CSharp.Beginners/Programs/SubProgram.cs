using CSharp.Beginners.Commands;

namespace CSharp.Beginners.Programs;

public abstract class SubProgram
{
    protected static bool CheckForErrors(ICommandResult commandResult, TextWriter output)
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