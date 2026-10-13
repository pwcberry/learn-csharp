namespace CSharp.Beginners.Commands;

public interface ICommand
{
    public ICommandResult Parse(string[] args);
}
