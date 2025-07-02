using aoc2024.helpers;

namespace aoc2024.solutions;

public class Day12 : Day
{
  public override void Part1()
  {
    var grid = new Grid(GetInputLines());
    var regions = GetRegions(grid);
    var cost = 0;
    foreach (var region in regions)
    {
      cost += region.Count * GetFencing(region);
    }

    Answer(cost);
  }

  public override void Part2()
  {
    var grid = new Grid(GetInputLines());
    var regions = GetRegions(grid);
    var cost = 0;
    foreach (var region in regions)
    {
      cost += region.Count * GetCorners(region);
    }

    Answer(cost);
  }

  private static HashSet<HashSet<(int x, int y)>> GetRegions(Grid grid)
  {
    var regions = new HashSet<HashSet<(int x, int y)>>();
    var seen = new HashSet<(int x, int y)>();
    var allSquares = grid.GetAllSquares();
    foreach (var square in allSquares)
    {
      if (seen.Contains((square.x, square.y)))
      {
        continue;
      }

      var found = Search(square, grid, [(square.x, square.y)]);
      seen.UnionWith(found);
      regions.Add(found);
    }

    return regions;
  }

  private static HashSet<(int x, int y)> Search((int x, int y, char c) startPosition, Grid grid,
    HashSet<(int x, int y)> included)
  {
    var neighbours = grid.GetFourAround((startPosition.x, startPosition.y))
      .Where(n => n.Item3 is not null && n.Item3 == startPosition.c);
    foreach (var neighbour in neighbours)
    {
      if (!included.Contains((neighbour.Item1, neighbour.Item2)))
      {
        included.Add((neighbour.Item1, neighbour.Item2));
        included.UnionWith(Search((neighbour.Item1, neighbour.Item2, neighbour.Item3 ?? throw new Exception()), grid,
          included));
      }
    }

    return included;
  }

  private static int GetFencing(HashSet<(int x, int y)> region)
  {
    var total = 0;
    foreach (var square in region)
    {
      if (!region.Contains((square.x + 1, square.y)))
      {
        total++;
      }

      if (!region.Contains((square.x - 1, square.y)))
      {
        total++;
      }

      if (!region.Contains((square.x, square.y + 1)))
      {
        total++;
      }

      if (!region.Contains((square.x, square.y - 1)))
      {
        total++;
      }
    }

    return total;
  }

  private static int GetCorners(HashSet<(int x, int y)> region)
  {
    var total = 0;
    foreach (var square in region)
    {
      if (region.Contains((square.x, square.y - 1)))
      {
        // Top left concave
        if (region.Contains((square.x - 1, square.y)) && !region.Contains((square.x - 1, square.y - 1)))
        {
          total++;
        }

        // Top right concave
        if (region.Contains((square.x + 1, square.y)) && !region.Contains((square.x + 1, square.y - 1)))
        {
          total++;
        }
      }

      if (region.Contains((square.x, square.y + 1)))
      {
        // Bottom left concave
        if (region.Contains((square.x - 1, square.y)) && !region.Contains((square.x - 1, square.y + 1)))
        {
          total++;
        }

        // Bottom right concave
        if (region.Contains((square.x + 1, square.y)) && !region.Contains((square.x + 1, square.y + 1)))
        {
          total++;
        }
      }

      // Top right convex
      if (!region.Contains((square.x, square.y - 1)) &&
          !region.Contains((square.x + 1, square.y)))
      {
        total++;
      }

      // Bottom right convex
      if (!region.Contains((square.x, square.y + 1)) &&
          !region.Contains((square.x + 1, square.y)))
      {
        total++;
      }

      // Bottom left convex
      if (!region.Contains((square.x, square.y + 1)) &&
          !region.Contains((square.x - 1, square.y)))
      {
        total++;
      }

      // Top left convex
      if (!region.Contains((square.x, square.y - 1)) &&
          !region.Contains((square.x - 1, square.y)))
      {
        total++;
      }
    }

    return total;
  }
}