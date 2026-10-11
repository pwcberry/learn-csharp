namespace CSharp.Beginners;

public readonly struct Point(int x, int y) : IEquatable<Point>
{
    public int X { get; init; } = x;
    public int Y { get; init; } = y;
    
    public double DistanceTo(Point other)
    {
        var dx = X - other.X;
        var dy = Y - other.Y;
        return Math.Sqrt(dx * dx + dy * dy);
    }
    
    public bool Equals(Point other) => X == other.X && Y == other.Y;
    
    public override bool Equals(object? obj) => obj is Point other && Equals(other);
    
    public override int GetHashCode() => HashCode.Combine(X, Y);
    
    public static bool operator ==(Point left, Point right) => left.Equals(right);
    public static bool operator !=(Point left, Point right) => !left.Equals(right);
}
