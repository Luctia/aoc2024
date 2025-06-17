using System.Text.RegularExpressions;

namespace aoc2024.solutions;

public partial class Day13 : Day
{
  public override void Part1()
  {
    var machines = GetClawMachines();
    long total = 0;
    foreach (var machine in machines)
    {
      total += machine.GetCost();
    }

    Answer(total);
  }

  public override void Part2()
  {
    var machines = GetClawMachines();
    long total = 0;
    foreach (var machine in machines)
    {
      machine.ActivatePartB();
      total += machine.GetCost();
    }

    Answer(total);
  }

  private HashSet<ClawMachine> GetClawMachines()
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
    return groups.Select(g => new ClawMachine(g)).ToHashSet();
  }

  private partial class ClawMachine
  {
    public (int x, int y) ButtonA;
    public (int x, int y) ButtonB;
    public (long x, long y) Prize;

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

    public void ActivatePartB()
    {
      Prize.x += 10000000000000;
      Prize.y += 10000000000000;
    }

    public long GetCost()
    {
      var aMax = Math.Max(Prize.x / ButtonA.x, Prize.y / ButtonA.y);
      var bMax = Math.Max(Prize.x / ButtonB.x, Prize.y / ButtonB.y);
      var candidates = new List<(long a, long b)>();
      for (long a = 0; a < aMax; a++)
      {
        for (long b = 0; b < bMax; b++)
        {
          if (Prize.x == ButtonA.x * a + ButtonB.x * b && Prize.y == ButtonA.y * a + ButtonB.y * b)
          {
            candidates.Add((a, b));
          }
        }
      }

      if (candidates.Count == 0)
      {
        return 0;
      }
      return candidates.Select(c => 3 * c.a + c.b).Min();
    }

    [GeneratedRegex(@"^.*X.(\d+), Y.(\d+)$")]
    private static partial Regex MyRegex();
  }
}