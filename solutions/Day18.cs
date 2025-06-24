using System.Text;
using aoc2024.helpers;

namespace aoc2024.solutions;

public class Day18 : Day
{
  public override void Part1()
  {
    const int gridHeight = 71;
    const int gridWidth = 71;
    var walls = GetInputLines()
      .Select(l => l.Split(','))
      .Select(d => (int.Parse(d[0]), int.Parse(d[1])))
      .ToArray()[..1024];
    List<string> lines = [];
    for (var y = 0; y < 71; y++)
    {
      var s = new StringBuilder();
      for (var x1 = 0; x1 < 71; x1++)
      {
        s.Append(walls.Contains((x: x1, y)) ? '#' : '.');
      }
      lines.Add(s.ToString());
    }

    var grid = new Grid(lines.ToArray());
    HashSet<(int x, int y)> seenTiles = [(0, 0)];
    HashSet<(int x, int y)> lastSeen = [(0, 0)];
    var steps = 0;
    while (!seenTiles.Contains((gridWidth - 1, gridHeight - 1)))
    {
      lastSeen = lastSeen
        .Select(t => grid.GetFourAround(t).Where(t => t.Item3 != '#' && t.Item3 is not null))
        .Select(t => t.Select(x => (x.x, x.y)))
        .Aggregate(new HashSet<(int x, int y)>(), (acc, t) => { acc.UnionWith(t); return acc; })
        .Where(t => !seenTiles.Contains(t))
        .ToHashSet();
      seenTiles.UnionWith(lastSeen);
      steps++;
    }
    Answer(steps);
  }

  public override void Part2()
  {
    var addedUpTo = 1025;
    const int gridHeight = 71;
    const int gridWidth = 71;
    HashSet<(int x, int y)> seenTiles = [];
    while (true)
    {
      var walls = GetInputLines()
        .Select(l => l.Split(','))
        .Select(d => (int.Parse(d[0]), int.Parse(d[1])))
        .ToArray()[..addedUpTo];
      if (seenTiles.Count > 0 && !seenTiles.Contains(walls[addedUpTo - 1]))
      {
        addedUpTo++;
        continue;
      }
      List<string> lines = [];
      for (var y = 0; y < 71; y++)
      {
        var s = new StringBuilder();
        for (var x1 = 0; x1 < 71; x1++)
        {
          s.Append(walls.Contains((x: x1, y)) ? '#' : '.');
        }
        lines.Add(s.ToString());
      }

      var grid = new Grid(lines.ToArray());
      seenTiles = [(0, 0)];
      HashSet<(int x, int y)> lastSeen = [(0, 0)];
      while (!seenTiles.Contains((gridWidth - 1, gridHeight - 1)))
      {
        var lengthBefore = seenTiles.Count;
        lastSeen = lastSeen
          .Select(t => grid.GetFourAround(t).Where(t => t.Item3 != '#' && t.Item3 is not null))
          .Select(t => t.Select(x => (x.x, x.y)))
          .Aggregate(new HashSet<(int x, int y)>(), (acc, t) => { acc.UnionWith(t); return acc; })
          .Where(t => !seenTiles.Contains(t))
          .ToHashSet();
        seenTiles.UnionWith(lastSeen);
        if (seenTiles.Count == lengthBefore)
        {
          Answer($"{walls[addedUpTo - 1].Item1},{walls[addedUpTo - 1].Item2}");
          return;
        }
      }

      addedUpTo++;
    }
  }
}