using CSharp.Beginners.Programs;

namespace CSharp.Beginners.Tests.Programs;

public class BankingProgramTests : CultureScope
{
    // Sum of the deposits (23,300) minus the withdrawals (4,050) in the seeded transaction records.
    private const string InitialBalance = "$19,250.00";

    private static string Run(BankingProgram program, params string[] args)
    {
        var output = new StringWriter();
        program.Run(args, output);
        return output.ToString();
    }

    [Fact]
    public void BalanceReportsTheSeededBalance()
    {
        var output = Run(new BankingProgram(), "balance");

        Assert.Equal($"Current bank balance: {InitialBalance}{Environment.NewLine}", output);
    }

    [Fact]
    public void DepositIncreasesTheBalance()
    {
        var program = new BankingProgram();

        var output = Run(program, "deposit", "750.25");

        Assert.Contains("Deposit of $750.25 successful. New balance: $20,000.25", output);
        Assert.Contains("Current bank balance: $20,000.25", Run(program, "balance"));
    }

    [Fact]
    public void WithdrawDecreasesTheBalance()
    {
        var program = new BankingProgram();

        var output = Run(program, "withdraw", "250");

        Assert.Contains("Withdrawal of $250.00 successful. New balance: $19,000.00", output);
        Assert.Contains("Current bank balance: $19,000.00", Run(program, "balance"));
    }

    [Fact]
    public void WithdrawingTheWholeBalanceIsAllowed()
    {
        var program = new BankingProgram();

        var output = Run(program, "withdraw", "19250");

        Assert.Contains("New balance: $0.00", output);
    }

    [Fact]
    public void WithdrawingMoreThanTheBalanceIsRejected()
    {
        var program = new BankingProgram();

        var output = Run(program, "withdraw", "19250.01");

        Assert.Contains("Error: Insufficient funds for this withdrawal.", output);
        Assert.Contains($"Current bank balance: {InitialBalance}", Run(program, "balance"));
    }

    [Theory]
    [InlineData("deposit")]
    [InlineData("withdraw")]
    public void NonNumericAmountsAreRejected(string command)
    {
        var program = new BankingProgram();

        var output = Run(program, command, "lots");

        Assert.Contains("Error: Invalid amount format. Please provide a valid decimal number.", output);
        Assert.Contains($"Current bank balance: {InitialBalance}", Run(program, "balance"));
    }

    [Theory]
    [InlineData("deposit")]
    [InlineData("withdraw")]
    public void MissingAmountsReportParseErrors(string command)
    {
        var output = Run(new BankingProgram(), command);

        Assert.StartsWith("Error: ", output);
    }

    [Fact]
    public void UnknownCommandsReportParseErrors()
    {
        var output = Run(new BankingProgram(), "transfer", "10");

        Assert.StartsWith("Error: ", output);
    }

    [Fact]
    public void HistoryListsSeededAndNewTransactions()
    {
        var program = new BankingProgram();
        Run(program, "deposit", "75");

        var lines = Run(program, "history").Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        Assert.Equal("Transaction history:", lines[0]);
        Assert.Equal("  $10,000.00(+): Initial balance", lines[1]);
        Assert.Equal("      $75.00(+): Deposit", lines[^1]);
        Assert.Equal(23, lines.Length);
    }

    [Fact]
    public void EachProgramStartsFromTheSeededBalance()
    {
        Run(new BankingProgram(), "withdraw", "1000");

        Assert.Contains($"Current bank balance: {InitialBalance}", Run(new BankingProgram(), "balance"));
    }
}

public class TransactionTests : CultureScope
{
    [Fact]
    public void DepositFormatsAsACredit()
    {
        Assert.Equal("     $500.00(+): pay", new Deposit(500m, "pay").ToString());
    }

    [Fact]
    public void WithdrawalFormatsAsADebit()
    {
        Assert.Equal("      $42.50(-): lunch", new Withdrawal(42.5m, "lunch").ToString());
    }
}
