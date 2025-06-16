using System.Text.RegularExpressions;

namespace aoc2024.solutions;

public partial class Day13 : Day
{
    public override void Part1()
    {
        var lines = GetInputLines();
        List<List<string>> groups = [];
        List<string> group = [];
        foreach (var line in lines)
        {
            if (string.IsNullOrEmpty(line))
            {
                groups.Add(group);
                group = [];
            }
            else
            {
                group.Add(line);
            }
        }
        groups.Add(group);
        var machines = groups.Select(g => new ClawMachine(g)).ToList();
    }

    public override void Part2()
    {
        throw new NotImplementedException();
    }

    private partial class ClawMachine
    {
        public (int x, int y) ButtonA;
        public (int x, int y) ButtonB;
        public (int x, int y) Prize;

        public ClawMachine(List<string> input)
        {
            ButtonA = ExtractNumbers(input[0]);
            ButtonB = ExtractNumbers(input[1]);
            Prize = ExtractNumbers(input[2]);
        }

        private static (int, int) ExtractNumbers(string line)
        {
            var numbers = MyRegex().Matches(line);
            return (int.Parse(numbers[0].Groups[1].Value), int.Parse(numbers[0].Groups[2].Value));
        }

        [GeneratedRegex(@"^.*X.(\d+), Y.(\d+)$")]
        private static partial Regex MyRegex();
    }
}