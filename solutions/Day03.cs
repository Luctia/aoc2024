using System.Text.RegularExpressions;

namespace aoc2024.solutions;

public class Day03 : Day
{
    public override void Part1()
    {
        var input = GetInput().Replace("\r", "").Replace("\n", "");
        const string pattern = @"mul\(\d+,\d+\)";
        var sum = 0;
        foreach (var match in Regex.Matches(input, pattern))
        {
            var nums = match.ToString()[4..^1].Split(",").Select(int.Parse).ToList();
            sum += nums[0] * nums[1];
        }
        Answer(sum);
    }

    public override void Part2()
    {
        var input = GetInput().Replace("\r", "").Replace("\n", "");
        const string pattern = @"(mul\(\d+,\d+\))|(do(n't)?\(\))";
        var sum = 0;
        var execute = true;
        foreach (var match in Regex.Matches(input, pattern))
        {
            switch (match.ToString())
            {
                case "do()":
                    execute = true;
                    continue;
                case "don't()":
                    execute = false;
                    continue;
            }

            if (execute)
            {
                var nums = match.ToString()[4..^1].Split(",").Select(int.Parse).ToList();
                sum += nums[0] * nums[1];
            }
        }
        Answer(sum);
    }
}