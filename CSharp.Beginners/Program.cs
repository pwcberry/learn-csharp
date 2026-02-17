using CSharp.Beginners;

// Collections.Basic();
//
// var set = Collections.CreateSortedSet(new string[] {
//     "Apple",
//     "alpha",
//     "gamma",
//     "zeta",
//     "Peach",
//     "group",
//     "Help",
//     "APPLE"
// });
//
// foreach(var item in set) {
//     Console.WriteLine(item);
// }

// Collections.SortAList();
// var numbers = Collections.GetFibonacciRange();
// foreach (var number in numbers)
// {
//     Console.WriteLine(number);
// }

double balance = 0;
foreach (var record in Collections.TransactionRecords(Collections.BankRecords))
{
    Console.WriteLine(record);
    balance += record switch
    {
        Deposit d => d.Amount,
        Withdrawal w => w.Amount,
        _ => 0
    };
}
Console.WriteLine("The account balance is: {0:C}", balance);