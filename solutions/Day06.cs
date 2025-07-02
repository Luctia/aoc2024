using aoc2024.helpers;

namespace aoc2024.solutions;

public class Day06 : Day
{
  public override void Part1()
  {
    var map = new Grid(GetInputLines());
    var positions = GetAllGuardPositions(map.GetGuardPosition(), Direction.Up, map);
    Answer(positions.Select(state => (state.x, state.y)).Distinct().Count());
  }

  public override void Part2()
  {
    var map = new Grid(GetInputLines());
    var guardPosition = map.GetGuardPosition();
    var positions = GetAllGuardPositions(guardPosition, Direction.Up, map)
      .Select(state => (state.x, state.y)).ToHashSet();
    var res = 0;
    positions.Remove(guardPosition);
    foreach (var position in positions)
    {
      var mapDupe = new Grid(GetInputLines());
      mapDupe.SetCharAt(position, '#');
      if (GuardLoops(guardPosition, Direction.Up, mapDupe))
      {
        res++;
      }
    }

    Answer(res);
  }

  private enum Direction
  {
    Up,
    Down,
    Left,
    Right
  }

  private static HashSet<(int x, int y, Direction)> GetAllGuardPositions((int x, int y) position, Direction direction,
    Grid grid)
  {
    HashSet<(int, int, Direction)> visited = [];
    while (!grid.IsOutOfBounds(position))
    {
      visited.Add((position.x, position.y, direction));
      var moved = MoveGuard(position, direction, grid);
      position = moved.Item1;
      direction = moved.Item2;
    }

    return visited;
  }

  private static bool GuardLoops((int x, int y) position, Direction direction, Grid grid)
  {
    HashSet<(int, int, Direction)> visited = [];
    while (!grid.IsOutOfBounds(position))
    {
      visited.Add((position.x, position.y, direction));
      var moved = MoveGuard(position, direction, grid);
      position = moved.newPosition;
      direction = moved.newDirection;
      if (visited.Contains((position.x, position.y, direction)))
      {
        return true;
      }
    }

    return false;
  }

  private static ((int x, int y) newPosition, Direction newDirection) MoveGuard((int x, int y) position,
    Direction direction, Grid grid)
  {
    switch (direction)
    {
      case Direction.Up:
      {
        position = (position.x, position.y - 1);
        if (grid.GetCharAt(position) == '#')
        {
          return ((position.x, position.y + 1), Direction.Right);
        }

        return (position, direction);
      }
      case Direction.Down:
      {
        position = (position.x, position.y + 1);
        if (grid.GetCharAt(position) == '#')
        {
          return ((position.x, position.y - 1), Direction.Left);
        }

        return (position, direction);
      }
      case Direction.Right:
      {
        position = (position.x + 1, position.y);
        if (grid.GetCharAt(position) == '#')
        {
          return ((position.x - 1, position.y), Direction.Down);
        }

        return (position, direction);
      }
      case Direction.Left:
      {
        position = (position.x - 1, position.y);
        if (grid.GetCharAt(position) == '#')
        {
          return ((position.x + 1, position.y), Direction.Up);
        }

        return (position, direction);
      }
      default: throw new Exception();
    }
  }
}