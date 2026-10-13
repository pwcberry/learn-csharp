using CSharp.Beginners.Programs;

namespace CSharp.Beginners.Tests.Programs;

public class CollectionsProgramTests
{
    private static string[] Run(params string[] args)
    {
        var output = new StringWriter();
        new CollectionsProgram().Run(args, output);
        return output.ToString().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
    }

    [Fact]
    public void HelloGreetsEachName()
    {
        Assert.Equal(["Hello, Ann!", "Hello, Bob!"], Run("hello", "Ann", "Bob"));
    }

    [Fact]
    public void SortOrdersItemsIgnoringCase()
    {
        Assert.Equal(["Sorted items:", "apple", "Banana", "cherry"], Run("sort", "cherry", "apple", "Banana"));
    }

    [Theory]
    [InlineData("10", "1, 1, 2, 3, 5, 8")]
    [InlineData("13", "1, 1, 2, 3, 5, 8, 13")]
    [InlineData("1", "1, 1")]
    public void FibonacciListsTheSequenceUpToTheMaximum(string maximum, string expected)
    {
        Assert.Equal([$"Fibonacci sequence up to {maximum}:", expected], Run("fibonacci", maximum));
    }

    [Theory]
    [InlineData("ten")]
    [InlineData("10", "20")]
    public void FibonacciRequiresASingleInteger(params string[] items)
    {
        Assert.Equal(["Error: Please provide a single integer for the Fibonacci sequence."], Run(["fibonacci", ..items]));
    }

    [Fact]
    public void MissingItemsReportParseErrors()
    {
        Assert.StartsWith("Error: ", Run("hello")[0]);
    }

    [Fact]
    public void UnknownCommandsReportParseErrors()
    {
        Assert.StartsWith("Error: ", Run("shuffle", "a")[0]);
    }
}

public class MyStringComparerTests
{
    private readonly MyStringComparer _comparer = new();

    [Theory]
    [InlineData("apple", "APPLE", true)]
    [InlineData("Apple", "apple", true)]
    [InlineData("apple", "pear", false)]
    [InlineData(null, null, true)]
    [InlineData("apple", null, false)]
    public void EqualsIgnoresCase(string? x, string? y, bool expected)
    {
        Assert.Equal(expected, _comparer.Equals(x, y));
    }

    [Theory]
    [InlineData("apple", "APPLE")]
    [InlineData("Banana", "bAnAnA")]
    public void EqualStringsHaveEqualHashCodes(string x, string y)
    {
        Assert.Equal(_comparer.GetHashCode(x), _comparer.GetHashCode(y));
    }

    [Fact]
    public void DistinctIgnoresCase()
    {
        Assert.Equal(["apple"], new[] { "apple", "APPLE", "Apple" }.Distinct(_comparer));
    }

    [Fact]
    public void CompareIgnoresCase()
    {
        Assert.Equal(0, _comparer.Compare("apple", "APPLE"));
        Assert.True(_comparer.Compare("apple", "Banana") < 0);
        Assert.True(_comparer.Compare("Cherry", "banana") > 0);
    }
}
