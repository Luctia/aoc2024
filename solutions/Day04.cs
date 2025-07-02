using aoc2024.helpers;

namespace aoc2024.solutions;

public class Day04 : Day
{
  public override void Part1()
  {
    var grid = new Grid(GetInputLines());
    var xs = grid.GetAllSquares().Where(x => x.Item3 == 'X').ToList();
    Answer(xs.Aggregate(0, (old, x) => GetXmasContinuations((x.Item1, x.Item2), grid) + old));
  }

  public override void Part2()
  {
    var grid = new Grid(GetInputLines());
    var centers = grid.GetAllSquares().Where(x => x.Item3 == 'A').ToList();
    var crosses = 0;
    foreach (var a in centers)
    {
      var pos = (a.Item1, a.Item2);
      var firstLine = (grid.AboveLeftOf(pos) == 'M' && grid.UnderRightOf(pos) == 'S') ||
                      (grid.AboveLeftOf(pos) == 'S' && grid.UnderRightOf(pos) == 'M');
      if (firstLine &&
          ((grid.AboveRightOf(pos) == 'M' && grid.UnderLeftOf(pos) == 'S') ||
           (grid.AboveRightOf(pos) == 'S' && grid.UnderLeftOf(pos) == 'M'))
         )
      {
        crosses++;
      }
    }

    Answer(crosses);
  }

  private static int GetXmasContinuations((int, int) coords, Grid grid)
  {
    var res = 0;
    res += grid.ReadUpwards(coords).Aggregate("", (a, b) => a + b).StartsWith("XMAS") ? 1 : 0;
    res += grid.ReadDownwards(coords).Aggregate("", (a, b) => a + b).StartsWith("XMAS") ? 1 : 0;
    res += grid.ReadLeft(coords).Aggregate("", (a, b) => a + b).StartsWith("XMAS") ? 1 : 0;
    res += grid.ReadRight(coords).Aggregate("", (a, b) => a + b).StartsWith("XMAS") ? 1 : 0;
    res += grid.ReadDownLeft(coords).Aggregate("", (a, b) => a + b).StartsWith("XMAS") ? 1 : 0;
    res += grid.ReadDownRight(coords).Aggregate("", (a, b) => a + b).StartsWith("XMAS") ? 1 : 0;
    res += grid.ReadUpLeft(coords).Aggregate("", (a, b) => a + b).StartsWith("XMAS") ? 1 : 0;
    res += grid.ReadUpRight(coords).Aggregate("", (a, b) => a + b).StartsWith("XMAS") ? 1 : 0;
    return res;
  }
}