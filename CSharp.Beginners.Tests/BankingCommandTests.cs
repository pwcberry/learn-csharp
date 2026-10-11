using CSharp.Beginners.Commands;

namespace CSharp.Beginners.Tests;

public class BankingCommandTests
{
    [Theory]
    [InlineData("deposit")]
    [InlineData("withdraw")]
    public void TransactionCommandsCollectTheAmount(string command)
    {
        var result = (BankingCommandResult) new BankingCommand().Parse([command, "250.50"]);

        Assert.Equal(command, result.Name);
        Assert.Equal("250.50", result.TransactionAmount);
        Assert.Empty(result.UnmatchedTokens);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public void BalanceCommandHasNoAmount()
    {
        var result = (BankingCommandResult) new BankingCommand().Parse(["balance"]);

        Assert.Equal("balance", result.Name);
        Assert.Null(result.TransactionAmount);
        Assert.Empty(result.Errors);
    }

    [Theory]
    [InlineData("deposit")]
    [InlineData("withdraw")]
    public void TransactionCommandsWithoutAnAmountAreErrors(string command)
    {
        var result = (BankingCommandResult) new BankingCommand().Parse([command]);

        Assert.NotEmpty(result.Errors);
    }

    [Fact]
    public void ExtraAmountsAreErrors()
    {
        var result = (BankingCommandResult) new BankingCommand().Parse(["deposit", "10", "20"]);

        Assert.NotEmpty(result.Errors);
    }

    [Fact]
    public void OptionsAreAlwaysEmpty()
    {
        var result = new BankingCommand().Parse(["deposit", "10"]);

        Assert.Empty(result.Options);
    }

    [Fact]
    public void UnknownCommandsAreErrors()
    {
        var result = new BankingCommand().Parse(["transfer", "10"]);

        Assert.NotEmpty(result.Errors);
    }
}
