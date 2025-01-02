using aoc2024.solutions;

namespace aoc2024;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        var watch = System.Diagnostics.Stopwatch.StartNew();
        new Day10().Part1();
        watch.Stop();
        Console.WriteLine(watch.ElapsedMilliseconds);
        watch = System.Diagnostics.Stopwatch.StartNew();
        new Day10().Part2();
        watch.Stop();
        Console.WriteLine(watch.ElapsedMilliseconds);
    }
}
