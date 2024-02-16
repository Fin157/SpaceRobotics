namespace SpaceRobotics;

public readonly struct Point(int x, int y)
{
    public readonly int x = x;
    public readonly int y = y;

    public double EuclideanDistance(Point other)
        => Math.Sqrt(Math.Pow(x - other.x, 2) + Math.Pow(y - other.y, 2));

    private Point Vector(Point end)
        => new(end.x - x, end.y - y);

    public static int DirectionQuotient(Point first, Point second, Point third)
    {
        Point referenceVector = first.Vector(second);
        double referenceDirection = referenceVector.y / referenceVector.x;
        Point wholeVector = first.Vector(third);
        double wholeVectorDirection = wholeVector.y / wholeVector.x;

        if (referenceDirection > wholeVectorDirection)
            return 1;
        else if (referenceDirection < wholeVectorDirection)
            return -1;
        else
            return 0;
    }

    public static bool operator ==(Point left, Point right)
        => left.Equals(right);

    public static bool operator !=(Point left, Point right)
        => !left.Equals(right);

    public override bool Equals(object obj)
    {
        if (obj is not Point) return false;

        Point other = (Point)obj;

        return EuclideanDistance(other) == 0;
    }

    public override int GetHashCode()
        => HashCode.Combine(x, y);

    /// <summary>
    /// Creates a point from its string representation.
    /// </summary>
    /// <param name="s">The source string of the point
    /// (accepts only "X Y" format, otherwise throws <see cref="FormatException"/>).</param>
    /// <returns>The created point.</returns>
    /// <exception cref="FormatException"/>
    public static Point SmartParse(string s)
    {
        string[] coordinates = s.Split(' ');

        if (coordinates.Length != 2 ||
            !int.TryParse(coordinates[0], out int x) || !int.TryParse(coordinates[1], out int y))
            throw new FormatException();

        return new(x, y);
    }

    public override readonly string ToString()
        => $"[{x};{y}]";
}