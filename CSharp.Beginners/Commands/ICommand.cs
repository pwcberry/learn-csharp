namespace CSharp.Beginners.Commands;

public interface ICommand
{
    public abstract ICommandResult Parse(string[] inputArguments);
}