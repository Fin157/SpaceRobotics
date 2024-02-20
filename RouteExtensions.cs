namespace SpaceRobotics;

/// <summary>
/// A class that provides extension methods for routes (linked lists of points)
/// </summary>
public static class RouteExtensions
{
    /// <summary>
    /// Removes all angles considered unconvex from the specified route.
    /// </summary>
    /// <param name="route">The route to be simplified.</param>
    /// <param name="swapDirection">Dictates which direction the method understands as unconvex.
    /// True for right paths (angles to the left are seen as unconvex) and vice versa.</param>
    public static void SimplifyRoute(this LinkedList<Point> route, bool swapDirection = false)
    {
        // Running this method on a route of two and less elements doesn't make sense as the
        // least number of points used in one iteration is three
        if (route.Count < 3) return;

        // Convert the direction boolean into a number
        int directionQuotient = swapDirection ? -1 : 1;

        // Set the initial working nodes
        var previous = route.First;
        var current = previous.Next;
        var next = current.Next;

        // Repeat until we reach the last node 
        while (current != route.Last)
        {
            // Calculate the angle between the three points
            double angle = Point.DirectionQuotient(previous.Value, current.Value, next.Value);

            // Checks if the angle is greater than 180 degress
            if (angle == directionQuotient)
                // Remove the current point from the linked list
                route.Remove(current);
            else
                // Move each of the node pointers one point further
                previous = current;

            current = next;
            next = current.Next;
        }

        // Make sure to remove all of the redundant unconvex points at the end
        if (previous == route.First) return;
        next = route.Last;

        while (true)
        {
            current = previous;
            previous = previous.Previous;

            if (Point.DirectionQuotient(previous.Value, current.Value, next.Value) == directionQuotient)
                route.Remove(current);
            else
                break;
        }
    }

    /// <summary>
    /// Calculates the distance driven using this route.
    /// </summary>
    /// <param name="route">The route as a linked list of points.</param>
    /// <returns>A floating-point number representing the length of the route.</returns>
    public static double RouteLength(this LinkedList<Point> route)
    {
        double length = 0;
        Point previous = default;

        foreach (var current in route)
        {
            if (previous != default)
                length += previous.EuclideanDistance(current);

            previous = current;
        }

        return length;
    }
}
