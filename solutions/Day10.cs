using aoc2024.helpers;

namespace aoc2024.solutions;

public class Day10 : Day
{
  public override void Part1()
  {
    Grid grid = new Grid(GetInputLines());
    (int, int)[] trailHeads = grid.GetAllPositions('0');
    Answer(trailHeads.Select(th => CalcScore(grid, th)).Aggregate(0, (x, y) => x + y));
  }

  public override void Part2()
  {
    Grid grid = new Grid(GetInputLines());
    (int, int)[] trailHeads = grid.GetAllPositions('0');
    Answer(trailHeads.Select(th => CalcRating(grid, th)).Aggregate(0, (x, y) => x + y));
  }

  private static int CalcScore(Grid grid, (int x, int y) trailHead)
  {
    List<(int x, int y)> heads = [trailHead];
    for (int i = 1; i < 10; i++)
    {
      HashSet<(int x, int y)> nextHeads = [];
      foreach (var head in heads)
      {
        nextHeads.UnionWith(
          grid.GetFourAround(head)
            .Where(x => int.Parse(x.Item3 == null ? "-1" : x.Item3.ToString()) == i)
            .Select(x => (x.Item1, x.Item2)));
      }

      heads = nextHeads.ToList();
    }

    return heads.Count;
  }

  private static int CalcRating(Grid grid, (int x, int y) trailHead)
  {
    List<(int x, int y)> heads = [trailHead];
    for (int i = 1; i < 10; i++)
    {
      List<(int x, int y)> nextHeads = [];
      foreach (var head in heads)
      {
        nextHeads.AddRange(
          grid.GetFourAround(head)
            .Where(x => int.Parse(x.Item3 == null ? "-1" : x.Item3.ToString()) == i)
            .Select(x => (x.Item1, x.Item2)));
      }

      heads = nextHeads;
    }

    return heads.Count;
  }
}