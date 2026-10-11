using CSharp.Beginners.Commands;

namespace CSharp.Beginners.Tests;

public class ArithmeticCommandsTests
{
    [Fact]
    public void SumCommandCollectsNumbersArgument()
    {
        var result = new ArithmeticCommands().Parse(["sum", "1", "2", "3"]);

        Assert.Equal("sum", result.Name);
        Assert.Equal(["1", "2", "3"], result.Numbers);
        Assert.Empty(result.UnmatchedTokens);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public void NonNumericArgumentsAreDropped()
    {
        var result = new ArithmeticCommands().Parse(["sum", "1", "abc", "2.5", "1e5", "NaN", "3"]);

        Assert.Equal(["1", "2.5", "3"], result.Numbers);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public void NegativeNumbersAreKept()
    {
        var result = new ArithmeticCommands().Parse(["sum", "-1", "-2.5"]);

        Assert.Equal(["-1", "-2.5"], result.Numbers);
    }

    [Fact]
    public void OptionsAreReadAfterTheSubcommand()
    {
        var result = new ArithmeticCommands().Parse(["sum", "1.5", "2", "-m", "2", "--precision", "3"]);

        Assert.Equal("2", result.Options["--multiply"]);
        Assert.Equal("3", result.Options["--precision"]);
        Assert.Equal(["1.5", "2"], result.Numbers);
    }

    [Fact]
    public void MissingOptionValuesAreNull()
    {
        var result = new ArithmeticCommands().Parse(["sum", "1"]);

        Assert.Null(result.Options["--multiply"]);
        Assert.Null(result.Options["--precision"]);
    }

    [Fact]
    public void SumWithNoNumbersIsEmpty()
    {
        var result = new ArithmeticCommands().Parse(["sum"]);

        Assert.Empty(result.Numbers);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public void UnknownTokensOnRootCommandAreErrors()
    {
        var result = new ArithmeticCommands().Parse(["bogus"]);

        Assert.NotEmpty(result.Errors);
    }
}
