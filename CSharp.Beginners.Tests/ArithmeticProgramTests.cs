using CSharp.Beginners.Programs;

namespace CSharp.Beginners.Tests;

public class ArithmeticProgramTests : CultureScope
{
    private const string SumRequired =
        "Error: The 'sum' command is required. Please provide the 'sum' subcommand followed by numbers to sum.";

    private const string RequiredCommandNotProvided = "Error: Required command was not provided.";
    
    
    private static string[] Run(params string[] args)
    {
        var output = new StringWriter();
        new ArithmeticProgram().Run(args, output);
        return output.ToString().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
    }

    [Fact]
    public void SumsIntegersWithAMultiplier()
    {
        Assert.Equal(
            ["Calculating sum on these numbers: 1, 2, 3", "Multiplier: 2, Precision: 0", "Sum: 12"],
            Run("sum", "1", "2", "3", "-m", "2"));
    }

    [Fact]
    public void SumsFloatsWithMultiplierAndPrecision()
    {
        Assert.Equal("Sum: 7.000", Run("sum", "1.5", "2", "--multiply", "2", "--precision", "3")[^1]);
    }

    [Fact]
    public void FloatMultiplierSwitchesToFloatingPointSum()
    {
        Assert.Equal("Sum: 7.5", Run("sum", "1", "2", "-m", "2.5", "-p", "1")[^1]);
    }

    [Fact]
    public void SumsWithoutOptions()
    {
        var output = Run("sum", "1", "2", "3");

        Assert.Equal("Multiplier: 1, Precision: 0", output[1]);
        Assert.Equal("Sum: 6", output[^1]);
    }

    [Fact]
    public void NonNumericArgumentsAreIgnored()
    {
        var output = Run("sum", "1", "abc", "3", "-m", "1");

        Assert.Equal("Calculating sum on these numbers: 1, 3", output[0]);
        Assert.Equal("Sum: 4", output[^1]);
    }

    [Fact]
    public void MissingSumCommandIsAnError()
    {
        var output = Run("-m", "2");
        Assert.Equal(RequiredCommandNotProvided, output[0]);
    }

    [Fact]
    public void UnknownCommandIsAnError()
    {
        var output = Run("product", "1", "2");
        Assert.Equal(RequiredCommandNotProvided, output[0]);
    }
}
