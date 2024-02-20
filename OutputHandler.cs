namespace SpaceRobotics;

public static class OutputHandler
{
    /// <summary>
    /// Writes the supplied output into the specified file or to the console
    /// if an unexistent file path is given.
    /// </summary>
    /// <param name="output">The output data to be written.</param>
    /// <param name="outputPath">Path to the output file.</param>
    public static void WriteOutput(LinkedList<Point> output, string outputPath = null)
    {
        // Obtain the target text reader instance
        using TextWriter sw = Path.Exists(outputPath) ? new StreamWriter(outputPath) : Console.Out;

        // Write the route into the output stream
        foreach (var point in output)
            sw.WriteLine(point);
    }
}