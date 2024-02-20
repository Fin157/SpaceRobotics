namespace SpaceRobotics;

public static class InputHandler
{
    public static void TakeInput(out Point startPoint, out Point endPoint, out Point[] polygon, out int closestToStart, out int closestToEnd, string inputPath = null)
    {
        using TextReader reader = Path.Exists(inputPath) ? new StreamReader(inputPath) : Console.In;

        // Get the start and end points
        startPoint = Point.SmartParse(reader.ReadLine());
        endPoint = Point.SmartParse(reader.ReadLine());

        // Initialize the array of points representing our polygon
        int polygonVertexCount = SmartParseInt(reader.ReadLine()); // Scrap the vertex count line because we don't need it
        double currentClosestToStartDist = double.MaxValue;
        double currentClosestToEndDist = double.MaxValue;
        closestToStart = -1;
        closestToEnd = -1;
        polygon = new Point[polygonVertexCount];

        // Fill the polygon array with its vertices and mark the closest one to the start point
        for (int i = 0; reader.Peek() != -1; i++)
        {
            Point p = Point.SmartParse(reader.ReadLine());
            polygon[i] = p;

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