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
    
    // todo maybe change to maps because looking for coordinates to connect to is faster

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
    
    // Remove reverses
    foreach (var path in straightPaths)
    {
      if (straightPaths.Contains(((path.end.x, path.end.y), (path.start.x, path.start.y), path.length)))
      {
        straightPaths.Remove(path);
      }
    }
    
    // Link straight paths
  }

  public override void Part2()
  {
    throw new NotImplementedException();
  }
}