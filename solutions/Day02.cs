namespace aoc2024.solutions;

public class Day02 : Day
{
    public override void Part1()
    {
        var parsedLines = GetInputLines()
            .Select(line => line.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse).ToArray());
        Answer(parsedLines.Where(IsInOrder).Count());
    }

    public override void Part2()
    {
        var parsedLines = GetInputLines()
            .Select(line => line.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse).ToArray());
        Answer(parsedLines.Count(l => IsInOrder(l) || CanBeDampened(l)));
    }

    private bool CanBeDampened(int[] ints)
    {
        return GetAllExclusions(ints).Any(i => IsInOrder(i.ToArray()));
    }

    private bool IsInOrder(int[] ints)
    {
        List<int> differences = [];
        for (int i = 0; i < ints.Length - 1; i++)
        {
            differences.Add(ints[i] - ints[i + 1]);
        }

        return (differences.All(d => d < 0) || differences.All(d => d > 0)) && 
               differences.All(d => Math.Abs(d) < 4 && Math.Abs(d) > 0);
    }

    private List<List<int>> GetAllExclusions(int[] ints)
    {
        var original = ints.ToList();
        var exclusions = new List<List<int>>();
        for (int i = 0; i < ints.Length; i++)
        {
            var temp = new List<int>(original);
            temp.RemoveAt(i);
            exclusions.Add(temp);
        }
        return exclusions;
    }
}