using TextCopy;

namespace aoc2024;

public abstract class Day
{
    public abstract void Part1();
    public abstract void Part2();

    protected string GetInput(string type = null) => File.ReadAllText($"inputs\\{GetType().Name}{type}.txt");

    protected string[] GetInputLines(string type = null) => GetInput(type).Split("\r\n");

    protected void Answer(object answer)
    {
        Console.WriteLine(GetType().Name + ": " +  answer);
        ClipboardService.SetText(answer.ToString() ?? "Not to-stringable");
    }
}
