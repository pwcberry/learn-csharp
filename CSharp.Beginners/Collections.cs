using System.Collections;
using System.Globalization;
using System.Text;

namespace CSharp.Beginners;

public static class Collections
{
    public static readonly string BankRecords = """
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
    
    public static void Basic()
    {
        List<string> names = ["<name>", "Ana", "Felipe"];
        foreach (var name in names)
        {
            Console.WriteLine($"Hello {name.ToUpper()}!");
        }
    }

    public static void SortAList()
    {
        var list = new List<string> { "apple", "Apple", "rouge", "Tea", "Humphrey" };
        list.Sort(new MyStringComparer());

        foreach (var item in list)
        {
            Console.WriteLine(item);
        }
    }

    public static HashSet<string> CreateHashSet(IEnumerable<string> items)
    {
        return new HashSet<string>(items, new MyStringComparer());
    }

    public static SortedSet<string> CreateSortedSet(IEnumerable<string> items)
    {
        return new SortedSet<string>(items, new MyStringComparer());
    }

    public static IEnumerable<int> GetFibonacciRange(int n = 20)
    {
        List<int> numbers = [1, 1];

        while (numbers.Count < n)
        {
            numbers.Add(numbers[^2] + numbers[^1]);
        }

        return numbers;
    }

    public static IEnumerable<object?> TransactionRecords(string inputText)
    {
        var reader = new StringReader(inputText);
        string? line;
        while ((line = reader.ReadLine()) is not null)
        {
            var parts = line.Split(',').Select(x => x.Trim()).ToArray();
            if (double.TryParse(parts[1], out var amount))
            {
                yield return parts[0] switch
                {
                    "DEPOSIT" => new Deposit(amount, parts[2]),
                    "WITHDRAWAL" => new Withdrawal(amount, parts[2]),
                    _ => null
                };
            }
        }
    }
}

public record Deposit(double Amount, string Description)
{
    public override string ToString() =>  $"{Amount,12:C}(+): {Description}";
}

public record Withdrawal(double Amount, string Description)
{
    public override string ToString() =>  $"{Amount,12:C}(-): {Description}";
}

/// <summary>
/// Ensures strings are equal or comparable no matter their case.
/// </summary>
public class MyStringComparer : IEqualityComparer<string>, IComparer<string>
{
    public int Compare(string? x, string? y)
    {
        return x?.ToUpper()?.CompareTo(y?.ToUpper()) ?? 0;
    }
    
    public bool Equals(string? x, string? y)
    {
         return string.Equals(x?.ToUpper(), y?.ToUpper());
    }

    public int GetHashCode(string obj)
    {
        return obj.GetHashCode();
    }
}