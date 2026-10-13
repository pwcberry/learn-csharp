using CSharp.Beginners.Commands;

namespace CSharp.Beginners.Tests.Commands;

public class TextFileCommandTests
{
    [Fact]
    public void ReadCommandObtainsTheEntireFileAsAString()
    {
        var result = (TextFileCommandResult) new TextFileCommand().Parse(["read", "--mode", "all", "test.txt"]);

        Assert.Equal("read", result.Name);
        Assert.Equal(ReadMode.All, result.Mode);
        Assert.Equal("test.txt", result.Arguments.First());
        Assert.Empty(result.UnmatchedTokens);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public void ReadCommandObtainsTheEntireFileAsAStringWithModeAlias()
    {
        var result = (TextFileCommandResult) new TextFileCommand().Parse(["read", "-m", "all", "test.txt"]);

        Assert.Equal("read", result.Name);
        Assert.Equal(ReadMode.All, result.Mode);
        Assert.Equal("test.txt", result.Arguments.First());
        Assert.Empty(result.UnmatchedTokens);
        Assert.Empty(result.Errors);
    }
    
    [Fact]
    public void ReadCommandExtractsASpecificLineFromTheFile()
    {
        var result = (TextFileCommandResult) new TextFileCommand().Parse(["read", "--mode", "line", "5", "test.txt"]);

        Assert.Equal("read", result.Name);
        Assert.Equal(ReadMode.Line, result.Mode);
        Assert.Equal("5", result.Arguments.First());
        Assert.Equal("test.txt", result.Arguments.Last());
        Assert.Empty(result.UnmatchedTokens);
        Assert.Empty(result.Errors);
    }
    
    [Fact]
    public void ReadCommandExtractsARangeOfLinesFromTheFile()
    {
        var result = (TextFileCommandResult) new TextFileCommand().Parse(["read", "--mode", "lines", "5", "10", "test.txt"]);

        Assert.Equal("read", result.Name);
        Assert.Equal(ReadMode.Lines, result.Mode);
        Assert.Equal("5", result.Arguments.First());
        Assert.Equal("10", result.Arguments.Skip(1).First());
        Assert.Equal("test.txt", result.Arguments.Last());
        Assert.Empty(result.UnmatchedTokens);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public void ReadCommandWithMissingModeRetainsArgumentsWithErrors()
    {
        var result = (TextFileCommandResult) new TextFileCommand().Parse(["read", "test.txt"]);

        Assert.Equal("read", result.Name);
        Assert.Equal(ReadMode.Error, result.Mode);
        Assert.Equal("test.txt", result.Arguments.First());
        Assert.Empty(result.UnmatchedTokens);
        Assert.NotEmpty(result.Errors);
    }
    
    [Fact]
    public void ReadCommandWithInvalidModeReturnsWithErrors()
    {
        var result = (TextFileCommandResult) new TextFileCommand().Parse(["read", "--mode", "invalid", "test.txt"]);

        Assert.Equal("read", result.Name);
        Assert.Equal(ReadMode.Error, result.Mode);
        Assert.Equal("test.txt", result.Arguments.First());
        Assert.Empty(result.UnmatchedTokens);
        Assert.NotEmpty(result.Errors);
    }
}