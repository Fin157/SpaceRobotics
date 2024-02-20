using SpaceRobotics;
using static SpaceRobotics.InputHandler;
using static SpaceRobotics.OutputHandler;

Execute(@"C:\Users\444St\source\repos\SpaceRobotics\input.txt", @"C:\Users\444St\source\repos\SpaceRobotics\output.txt");

// The driver function of the whole program
void Execute(string staticInputPath = null, string staticOutputPath = null)
{
    // Prepare paths to input and output
    string usedInputPath = staticInputPath ?? TakePath("Input file path (leave blank for console input): ");
    string usedOutputPath = staticOutputPath ?? TakePath("Output file path (leave blank for console output): ");

    // Data in
    TakeInput(out Point startPoint, out Point endPoint, out Point[] polygon, out int closestToStart, out int closestToEnd, usedInputPath);

    // Data processing
    LinkedList<Point> path = Calculate(startPoint, endPoint, polygon, closestToStart, closestToEnd);

    // Data out
    WriteOutput(path, usedOutputPath);
}

// The main calculation function of the program
LinkedList<Point> Calculate(Point startPoint, Point endPoint, Point[] polygon, int startConnectionIndex, int endConnectionIndex)
{
    // Split the polygon into two separate routes for the vehicle to drive along
    var (leftRoute, rightRoute) = GetRoutes(polygon, startPoint, endPoint, startConnectionIndex, endConnectionIndex);

    // Simplify both routes
    leftRoute.SimplifyRoute(true);
    Console.WriteLine();
    rightRoute.SimplifyRoute();

    // Return whichever of the two routes is shorter
    return leftRoute.RouteLength() < rightRoute.RouteLength() ? leftRoute : rightRoute;
}

// Splits a single polygon (an array of points) into two independent linked lists,
// each of which is a route for the vehicle to take
(LinkedList<Point> leftRoute, LinkedList<Point> rightRoute) GetRoutes(Point[] polygon, Point start, Point end, int startConnectionIndex, int endConnectionIndex)
{
    LinkedList<Point> first = [];
    LinkedList<Point> second = [];

    // Calculate wrapped versions of connection indices
    int endConnectionIndexWrapped = startConnectionIndex > endConnectionIndex ?
        endConnectionIndex + polygon.Length : endConnectionIndex;
    int startConnectionIndexWrapped = startConnectionIndex < endConnectionIndex ?
        startConnectionIndex + polygon.Length : startConnectionIndex;

    // Fill the first route
    for (int i = endConnectionIndex; i <= startConnectionIndexWrapped; i++)
        first.AddFirst(polygon[i % polygon.Length]);

    // Fill the second route
    for (int i = startConnectionIndex; i <= endConnectionIndexWrapped; i++)
        second.AddLast(polygon[i % polygon.Length]);

    // Add the start and end point to each of the routes
    // (the vehicle's initial position and its destination, respectively)
    second.AddFirst(start);
    first.AddFirst(start);
    second.AddLast(end);
    first.AddLast(end);

    return (first, second);
}