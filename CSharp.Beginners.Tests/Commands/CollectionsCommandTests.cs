using CSharp.Beginners.Commands;

namespace CSharp.Beginners.Tests.Commands;

public class CollectionsCommandTests
{
    [Theory]
    [InlineData("hello")]
    [InlineData("sort")]
    [InlineData("fibonacci")]
    public void SubcommandsCollectTheItemsArgument(string command)
    {
        var result = (CollectionsCommandResult) new CollectionsCommand().Parse([command, "a", "b", "c"]);

        Assert.Equal(command, result.Name);
        Assert.Equal(["a", "b", "c"], result.Items);
        Assert.Empty(result.UnmatchedTokens);
        Assert.Empty(result.Errors);
    }

    [Theory]
    [InlineData("hello")]
    [InlineData("sort")]
    [InlineData("fibonacci")]
    public void SubcommandsWithoutItemsAreErrors(string command)
    {
        var result = new CollectionsCommand().Parse([command]);

        Assert.NotEmpty(result.Errors);
    }

    [Fact]
    public void OptionsAreAlwaysEmpty()
    {
        var result = new CollectionsCommand().Parse(["hello", "Ann"]);

        Assert.Empty(result.Options);
    }

    [Fact]
    public void UnknownCommandsAreErrors()
    {
        var result = new CollectionsCommand().Parse(["shuffle", "a", "b"]);

        Assert.NotEmpty(result.Errors);
    }

    [Fact]
    public void NoArgumentsHaveNoItems()
    {
        var result = (CollectionsCommandResult) new CollectionsCommand().Parse([]);

        Assert.Empty(result.Items);
    }
}
