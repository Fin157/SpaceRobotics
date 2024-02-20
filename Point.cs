namespace SpaceRobotics;

public readonly struct Point(int x, int y)
{
    public readonly int x = x;
    public readonly int y = y;

    /// <summary>
    /// Returns the euclidean distance between this and the specified point.
    /// </summary>
    /// <param name="other">The point to which we calculate the distance from this one.</param>
    /// <returns>The distance between the two points.</returns>
    public double EuclideanDistance(Point other)
        => Math.Sqrt(Math.Pow(x - other.x, 2) + Math.Pow(y - other.y, 2));

    /// <summary>
    /// Returns minimal information about the angle between three points (two vectors).
    /// </summary>
    /// <param name="first">The first point.</param>
    /// <param name="second">The second point.</param>
    /// <param name="third">The third point.</param>
    /// <returns>1 if the angle ABC (first, second, third) is convex to the right, -1 if it
    /// is convex to the left, 0 if the angle is 0 or 180 degrees.</returns>
    public static int DirectionQuotient(Point first, Point second, Point third)
    {
        // Internally build vectors out of the three given points
        Point v1 = first.Vector(second);
        Point v2 = first.Vector(third);

        // We know that the cross product operation in 2D gives us information about the angle
        // between the two vectors: is greater than zero if the rotational delta between the two
        // vectors is positive (to the left), is less than zero if the delta is to the right
        // and is precisely zero if the vectors are colinear
        int crossProduct = CrossProduct(v2, v1);

        // Return the cross product's sign to condense it into just 1, 0, -1
        return Math.Sign(crossProduct);
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
        => $"{x} {y}";

    /// <summary>
    /// An internal method to make a vector starting at this point and ending at given point.
    /// </summary>
    /// <param name="end">The end point of the vector.</param>
    /// <returns>A new point whose X and Y coordinates represent the dimensions of the vector.</returns>
    private Point Vector(Point end)
        => new(end.x - x, end.y - y);

    /// <summary>
    /// An internal method to calculate the 2D cross product of two vectors.
    /// </summary>
    /// <param name="v1">The first vector.</param>
    /// <param name="v2">The second vector.</param>
    /// <returns>The result of the cross product operation.</returns>
    private static int CrossProduct(Point v1, Point v2)
        => v1.x * v2.y - v2.x * v1.y;
}