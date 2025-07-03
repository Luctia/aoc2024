using aoc2024.helpers;

namespace aoc2024.solutions;

public class Day16 : Day
{
  public override void Part1()
  {
    var grid = new Grid(GetInputLines());
    var currentPos = grid.GetGuardPosition('S');
    var end = grid.GetGuardPosition('E');
    var corners = grid.GetCorners();
    var straightPaths = new HashSet<((int x, int y) start, (int x, int y) end, int length)>();
    
    // todo missing 

    for (int i = 0; i < corners.Length; i++)
    {
      for (int j = i + 1; j < corners.Length; j++)
      {
        if (corners[i].y == corners[j].y)
        {
          straightPaths.Add(((corners[i].x, corners[i].y), (corners[j].x, corners[j].y), Math.Abs(corners[i].x - corners[j].x)));
        }
        else if (corners[i].x == corners[j].x)
        {
          straightPaths.Add(((corners[i].x, corners[i].y), (corners[j].x, corners[j].y), Math.Abs(corners[i].y - corners[j].y)));
        }
      }
    }
    
    var tilesToStraights = new Dictionary<(int x, int y), ISet<((int x, int y) start, (int x, int y) end, int length)>>();
    foreach (var path in straightPaths)
    {
      if (tilesToStraights.ContainsKey((path.start.x, path.start.y)))
      {
        tilesToStraights[(path.start.x, path.start.y)].Add(path);
      }
      else
      {
        tilesToStraights[(path.start.x, path.start.y)] = new HashSet<((int x, int y) start, (int x, int y) end, int length)> { path };
      }
    }
  }

  public override void Part2()
  {
    throw new NotImplementedException();
  }
}