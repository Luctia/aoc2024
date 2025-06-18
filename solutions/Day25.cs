namespace aoc2024.solutions;

public class Day25 : Day
{
    public override void Part1()
    {
        var lines = GetInputLines();
        List<List<string>> keys = [];
        List<List<string>> locks = [];
        List<string> entry = [];
        for (int i = 0; i < lines.Length; i++)
        {
            if (string.IsNullOrEmpty(lines[i]))
            {
                if (entry[0][0] == '.')
                {
                    keys.Add(entry);
                }
                else
                {
                    locks.Add(entry);
                }
                entry = [];
            }
            else
            {
                entry.Add(lines[i]);
            }
        }
        var keyNumbers = keys.Select(GetNumbers).ToArray();
        var lockNumbers = locks.Select(GetNumbers).ToArray();
        var pairsfound = 0;

        foreach (var lockNumber in lockNumbers)
        {
            pairsfound += keyNumbers.Count(k => k[0] + lockNumber[0] <= 5 && k[1] + lockNumber[1] <= 5 && k[2] + lockNumber[2] <= 5 && k[3] + lockNumber[3] <= 5 && k[4] + lockNumber[4] <= 5);
        }
        
        Answer(pairsfound);
    }

    public override void Part2()
    {
        throw new NotImplementedException();
    }

    private static short[] GetNumbers(List<string> input)
    {
        short[] res = [-1, -1, -1, -1, -1];
        foreach (var line in input)
        {
            for (var i = 0; i < line.Length; i++)
            {
                if (line[i] == '#')
                {
                    res[i]++;
                }
            }
        }
        return res;
    }
}