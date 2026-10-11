using CSharp.Beginners.Commands;

namespace CSharp.Beginners.Programs;

public class BankingProgram: SubProgram<BankingCommand, BankingCommandResult>
{
    private static readonly string _transactionRecords = """
 DEPOSIT,   10000, Initial balance
 DEPOSIT,     500, regular deposit
 WITHDRAWAL, 1000, rent
 DEPOSIT,    2000, freelance payment
 WITHDRAWAL,  300, groceries
 DEPOSIT,     700, gift from friend
 WITHDRAWAL,  150, utility bill
 DEPOSIT,    1200, tax refund
 WITHDRAWAL,  500, car maintenance
 DEPOSIT,     400, cashback reward
 WITHDRAWAL,  250, dining out
 DEPOSIT,    3000, bonus payment
 WITHDRAWAL,  800, loan repayment
 DEPOSIT,     600, stock dividends
 WITHDRAWAL,  100, subscription fee
 DEPOSIT,    1500, side hustle income
 WITHDRAWAL,  200, fuel expenses
 DEPOSIT,     900, refund from store
 WITHDRAWAL,  350, shopping
 DEPOSIT,    2500, project milestone payment
 WITHDRAWAL,  400, entertainment
 """;
    
    private readonly IList<ITransaction> _transactions = [];
    private decimal _bankBalance = decimal.MinusOne;
    
    public BankingProgram()
    {
        ParseRecords();
        CalculateBankBalance();
    }
    
    public override void Run(string[] inputArguments, TextWriter output)
    {
        var (commandResult, hasErrors) = ParseCommand(inputArguments, output);

        // Any unknown commands will be reported as errors
        if (hasErrors)
            return;
        
        switch(commandResult.Name.ToLower())
        {
            case "balance":
                output.WriteLine($"Current bank balance: {_bankBalance:C}");
                break;
            case "history":
                output.WriteLine("Transaction history:");
                
                foreach (var transaction in _transactions)
                {
                    output.WriteLine(transaction);
                }
                break;
            case "withdraw":
                Withdraw(commandResult, output);
                break;
            case "deposit":
                Deposit(commandResult, output);
                break;
        }
    }

    private void Withdraw(BankingCommandResult commandResult, TextWriter output)
    {
        if (commandResult.TransactionAmount is null)
        {
            output.WriteLine("Error: Please provide an amount to withdraw.");
            return;
        }

        if (!decimal.TryParse(commandResult.TransactionAmount, out decimal withdrawAmount))
        {
            output.WriteLine("Error: Invalid amount format. Please provide a valid decimal number.");
            return;
        }
                
        if (withdrawAmount > _bankBalance)
        {
            output.WriteLine("Error: Insufficient funds for this withdrawal.");
            return;
        }
                
        var withdrawal = new Withdrawal(withdrawAmount, "Withdrawal");
        _transactions.Add(withdrawal);
        _bankBalance -= withdrawAmount;
        output.WriteLine($"Withdrawal of {withdrawAmount:C} successful. New balance: {_bankBalance:C}");
    }

    private void Deposit(BankingCommandResult commandResult, TextWriter output)
    {
        if (commandResult.TransactionAmount is null)
        {
            output.WriteLine("Error: Please provide an amount to deposit.");
            return;
        }

        if (!decimal.TryParse(commandResult.TransactionAmount, out decimal depositAmount))
        {
            output.WriteLine("Error: Invalid amount format. Please provide a valid decimal number.");
            return;
        }

        var deposit = new Deposit(depositAmount, "Deposit");
        _transactions.Add(deposit);
        _bankBalance += depositAmount;
        output.WriteLine($"Deposit of {depositAmount:C} successful. New balance: {_bankBalance:C}");
    }

    private void CalculateBankBalance()
    {
        _bankBalance = 0;
        foreach (var transaction in _transactions)
        {
            switch (transaction)
            {
                case Deposit deposit:
                    _bankBalance += deposit.Amount;
                    break;
                case Withdrawal withdrawal:
                    _bankBalance -= withdrawal.Amount;
                    break;
            }
        }
    }
    
    private void ParseRecords()
    {
        var reader = new StringReader(_transactionRecords);
        while (reader.ReadLine() is { } line)
        {
            var parts = line.Split(',', 3, StringSplitOptions.TrimEntries);
            if (parts.Length != 3)
            {
                continue;
            }

            var transactionType = parts[0].ToUpperInvariant();
            
            if (!decimal.TryParse(parts[1], out var amount))
            {
                continue;
            }

            var description = parts[2];
            
            ITransaction? transaction = transactionType switch
            {
                "DEPOSIT" => new Deposit(amount, description),
                "WITHDRAWAL" => new Withdrawal(amount, description),
                _ => null
            };

            if (transaction is not null)
            {
                _transactions.Add(transaction);
            }
        }
    }
}


public interface ITransaction
{
    public decimal Amount { get; }
}

public record Deposit(decimal Amount, string Description) : ITransaction
{
    public override string ToString() =>  $"{Amount,12:C}(+): {Description}";
}

public record Withdrawal(decimal Amount, string Description): ITransaction
{
    public override string ToString() =>  $"{Amount,12:C}(-): {Description}";
}