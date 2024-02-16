using SpaceRobotics;
using static SpaceRobotics.InputHandler;
using static SpaceRobotics.OutputHandler;

Execute(@"D:\Coding\Competitive\RandomSchoolComp\2024\SpaceRobotics\input.txt", @"D:\Coding\Competitive\RandomSchoolComp\2024\SpaceRobotics\output.txt");

// The driver function of the whole program
void Execute(string staticInputPath = null, string staticOutputPath = null)
{
    // Prepare paths to input and output
    string usedInputPath = staticInputPath ?? TakePath("Input file path (leave blank for console input): ");
    string usedOutputPath = staticOutputPath ?? TakePath("Output file path (leave blank for console output): ");

    TakeInput(out Point startPoint, out Point endPoint, out RingArray<Point> polygon, out int closestToStart, out int closestToEnd, usedInputPath);

    Stack<Point> path = Calculate(startPoint, endPoint, polygon, closestToStart, closestToEnd);

    WriteOutput(path, usedOutputPath);
}

Stack<Point> Calculate(Point startPoint, Point endPoint, RingArray<Point> polygon, int startConnectionIndex, int endConnectionIndex)
{
    foreach (Point p in polygon)
        Console.WriteLine(p);
    Console.WriteLine();

    // Calculate the left path starting with the start point and the connection point on the stack
    Stack<Point> path = [];
    Point previous = startPoint;
    path.Push(startPoint);
    int pathDirection = 1;

    for (int i = startConnectionIndex; i != endConnectionIndex; i += pathDirection)
    {
        Point current = polygon[i];
        Point next = i % (polygon.Length - 1) == endConnectionIndex ? endPoint : polygon[i + pathDirection];

        int direction = Point.DirectionQuotient(previous, current, next);

        if (direction != pathDirection)
        {
            path.Push(current);
            previous = current;
        }
        else
            path.Push(next);
    }

    foreach (Point point in path)
        Console.WriteLine(point);

    // Start traversing the array to the left (in order)


    // Calculate the right path as well with the same principle
    // Calculate the lengths of the paths and take the shorter one

    // For each route:
    // Start at start point
    // Add the next point in line to the route
    // Proceed to the next point
    // Calculate the angle AT the middle point (the one between the newest one and the starter)
    // If the angle > 180, revert the newest point and add just the one after it instead to the stack
    // Repeat until end point

    return [];
}