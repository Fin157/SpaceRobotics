namespace SpaceRobotics;

public static class InputHandler
{
    public static void TakeInput(out Point startPoint, out Point endPoint, out LinkedList<Point> leftRoute, out LinkedList<Point> rightRoute, string inputPath = null)
    {
        StreamReader reader = Path.Exists(inputPath) ? new(inputPath) : (StreamReader)Console.In;

        // Get the start and end points
        startPoint = Point.SmartParse(reader.ReadLine());
        endPoint = Point.SmartParse(reader.ReadLine());

        // Initialize the array of points representing our polygon
        int polygonVertexCount = SmartParseInt(reader.ReadLine()); // Scrap the vertex count line because we don't need it
        double currentClosestToStartDist = double.MaxValue;
        double currentClosestToEndDist = double.MaxValue;
        int closestToStart = -1;
        int closestToEnd = -1;
        LinkedList<Point> points = [];

        // Fill the polygon array with its vertices and mark the closest one to the start point
        for (int i = 0; !reader.EndOfStream; i++)
        {
            Point p = Point.SmartParse(reader.ReadLine());
            points.AddLast(p);

            // Set this point as the current closest if it is closer than the previous closest
            double distanceToStart = p.EuclideanDistance(startPoint);
            double distanceToEnd = p.EuclideanDistance(endPoint);
            if (distanceToStart < currentClosestToStartDist)
            {
                closestToStart = i;
                currentClosestToStartDist = distanceToStart;
            }
            if (distanceToEnd < currentClosestToEndDist)
            {
                closestToEnd = i;
                currentClosestToEndDist = distanceToEnd;
            }
        }

        leftRoute = [];
        rightRoute = [];

        Point point = points.ElementAt(closestToStart);
        Point end = points.ElementAt(closestToEnd);
        while (point != end)
        {
            leftRoute.AddLast(point);
        }
        leftRoute.AddFirst(startPoint);
        leftRoute.AddLast()
        leftRoute.AddLast(endPoint);

        rightRoute.AddLast(startPoint);
    }

    public static string TakePath(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine();
    }

    private static int SmartParseInt(string s)
    {
        if (!int.TryParse(s, out int i))
            throw new FormatException();

        return i;
    }
}