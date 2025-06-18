using System.Text.RegularExpressions;

namespace aoc2024.solutions;

public partial class Day13 : Day
{
  public override void Part1()
  {
    var machines = GetClawMachines();
    Answer(machines.Sum(machine => machine.GetCost()));
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

  private partial class ClawMachine(List<string> input)
  {
    private readonly (int x, int y) _buttonA = ExtractNumbers(input[0]);
    private readonly (int x, int y) _buttonB = ExtractNumbers(input[1]);
    private (long x, long y) _prize = ExtractNumbers(input[2]);

    private static (int, int) ExtractNumbers(string line)
    {
      var numbers = MyRegex().Matches(line);
      return (int.Parse(numbers[0].Groups[1].Value), int.Parse(numbers[0].Groups[2].Value));
    }

    public void ActivatePartB()
    {
      _prize.x += 10000000000000;
      _prize.y += 10000000000000;
    }

    public long GetCost()
    {
      double[][] matrix = [
        [_buttonA.x, _buttonB.x, _prize.x],
        [_buttonA.y, _buttonB.y, _prize.y]];

      // first row to 1 _ _
      matrix[0][1] /= matrix[0][0];
      matrix[0][2] /= matrix[0][0];
      matrix[0][0] = 1;

      // second row to 0 _ _
      matrix[1][1] -= matrix[1][0] * matrix[0][1]; 
      matrix[1][2] -= matrix[1][0] * matrix[0][2];
      matrix[1][0] = 0;
      
      // second row to 0 1 _
      matrix[1][2] *= 1 / matrix[1][1];
      matrix[1][1] = 1;
      
      // first row to 1 0 _
      matrix[0][2] -= matrix[1][2] * (matrix[0][1] /  matrix[1][1]);
      matrix[0][1] -= matrix[1][1] * (matrix[0][1] /  matrix[1][1]);
      
      (double a, double b) crudeAns = (matrix[0][2],  matrix[1][2]);

      (long a, long b) ans = ((long)Math.Round(crudeAns.a), (long)Math.Round(crudeAns.b));

      if (_buttonA.x * ans.a + _buttonB.x * ans.b != _prize.x || _buttonA.y * ans.a + _buttonB.y * ans.b != _prize.y)
      {
        return 0;
      }
      
      return ans.a * 3 + ans.b;
    }

    [GeneratedRegex(@"^.*X.(\d+), Y.(\d+)$")]
    private static partial Regex MyRegex();
  }
}