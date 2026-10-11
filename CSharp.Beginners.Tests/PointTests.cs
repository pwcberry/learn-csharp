using CSharp.Beginners.Geometry;

namespace CSharp.Beginners.Tests;

public class PointTests
{
    [Fact]
    public void PointsWithSameCoordinatesAreEqual()
    {
        var a = new Point(2, 2);
        var b = new Point(2, 2);
        Assert.Equal(a, b);
    }
    
    [Fact]
    public void PointsWithDifferentCoordinatesAreNotEqual() 
    {
        var a = new Point(2, 2);
        var b = new Point(3, 3);
        Assert.NotEqual(a, b);
    }
    
    [Fact]
    public void DistanceBetweenPointsIsCalculatedCorrectly()
    {
        var a = new Point(0, 0);
        var b = new Point(3, 4);
        Assert.Equal(5, a.DistanceTo(b));
    }
}