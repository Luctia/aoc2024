namespace aoc2024.helpers;

internal class Grid(string[] lines)
{
  public string[] Lines { get; set; } = lines;

  public bool IsOutOfBounds((int x, int y) position)
  {
    return position.y < 0 || position.y >= Lines.Length || position.x < 0 || position.x >= Lines[0].Length;
  }

  public (int, int) GetGuardPosition(char guardIcon = '^')
  {
    var y = -1;
    for (var i = 0; i < Lines.Length; i++)
    {
      if (Lines[i].Contains(guardIcon))
      {
        y = i;
        break;
      }
    }

    var x = 0;
    for (var i = 0; i < Lines[y].Length; i++)
    {
      if (Lines[y][i] == guardIcon)
      {
        x = i;
      }
    }

    return (x, y);
  }

  public char GetCharAt((int x, int y) position)
  {
    if (IsOutOfBounds(position))
    {
      return '.';
    }

    return Lines[position.y][position.x];
  }

  public void SetCharAt((int x, int y) position, char value)
  {
    Lines[position.y] = Lines[position.y].Insert(position.x, value.ToString()).Remove(position.x + 1, 1);
  }

  public (int, int)[] GetAllPositions(char c)
  {
    var res = new (int, int)[Lines.Aggregate(0, (acc, l) => acc + l.Count(x => x == c))];
    var index = 0;
    for (var y = 0; y < Lines.Length; y++)
    {
      for (int x = 0; x < Lines[y].Length; x++)
      {
        if (Lines[y][x] == c)
        {
          res[index] = (x, y);
          index++;
        }
      }
    }

    return res;
  }

  public char? Above((int x, int y) position)
  {
    if (IsOutOfBounds((position.x, position.y - 1)))
    {
      return null;
    }

    return Lines[position.y - 1][position.x];
  }

  public char? Under((int x, int y) position)
  {
    if (IsOutOfBounds((position.x, position.y + 1)))
    {
      return null;
    }

    return Lines[position.y + 1][position.x];
  }

  public char? RightOf((int x, int y) position)
  {
    if (IsOutOfBounds((position.x + 1, position.y)))
    {
      return null;
    }

    return Lines[position.y][position.x + 1];
  }

  public char? LeftOf((int x, int y) position)
  {
    if (IsOutOfBounds((position.x - 1, position.y)))
    {
      return null;
    }

    return Lines[position.y][position.x - 1];
  }

  public char? AboveRightOf((int x, int y) position)
  {
    var pos = (position.x + 1, position.y - 1);
    if (IsOutOfBounds(pos))
    {
      return null;
    }

    return Lines[pos.Item2][pos.Item1];
  }

  public char? AboveLeftOf((int x, int y) position)
  {
    var pos = (position.x - 1, position.y - 1);
    if (IsOutOfBounds(pos))
    {
      return null;
    }

    return Lines[pos.Item2][pos.Item1];
  }

  public char? UnderRightOf((int x, int y) position)
  {
    var pos = (position.x + 1, position.y + 1);
    if (IsOutOfBounds(pos))
    {
      return null;
    }

    return Lines[pos.Item2][pos.Item1];
  }

  public char? UnderLeftOf((int x, int y) position)
  {
    var pos = (position.x - 1, position.y + 1);
    if (IsOutOfBounds(pos))
    {
      return null;
    }

    return Lines[pos.Item2][pos.Item1];
  }

  public List<(int x, int y, char c)> GetAllSquares()
  {
    List<(int, int, char)> res = new();
    for (int y = 0; y < Lines.Length; y++)
    {
      for (int x = 0; x < Lines[y].Length; x++)
      {
        res.Add((x, y, Lines[y][x]));
      }
    }

    return res;
  }

  public List<(int, int, char)> GetEightAround((int x, int y) coords)
  {
    List<(int, int, char)> res =
    [
      (coords.x - 1, coords.y - 1, Lines[coords.y - 1][coords.x - 1]),
      (coords.x, coords.y - 1, Lines[coords.y - 1][coords.x]),
      (coords.x + 1, coords.y - 1, Lines[coords.y - 1][coords.x + 1]),
      (coords.x - 1, coords.y, Lines[coords.y][coords.x - 1]),
      (coords.x + 1, coords.y, Lines[coords.y][coords.x + 1]),
      (coords.x - 1, coords.y + 1, Lines[coords.y + 1][coords.x - 1]),
      (coords.x, coords.y + 1, Lines[coords.y + 1][coords.x]),
      (coords.x + 1, coords.y + 1, Lines[coords.y + 1][coords.x + 1])
    ];
    return res;
  }

  public HashSet<(int x, int y, char?)> GetFourAround((int x, int y) coords)
  {
    HashSet<(int, int, char?)> res =
    [
      (coords.x, coords.y - 1, Above(coords)),
      (coords.x - 1, coords.y, LeftOf(coords)),
      (coords.x + 1, coords.y, RightOf(coords)),
      (coords.x, coords.y + 1, Under(coords))
    ];
    return res;
  }

  public IEnumerable<char> ReadUpwards((int x, int y) start)
  {
    List<char> res = [];
    while (!IsOutOfBounds(start))
    {
      res.Add(Lines[start.y][start.x]);
      start = (start.x, start.y - 1);
    }

    return res;
  }

  public IEnumerable<char> ReadDownwards((int x, int y) start)
  {
    List<char> res = [];
    while (!IsOutOfBounds(start))
    {
      res.Add(Lines[start.y][start.x]);
      start = (start.x, start.y + 1);
    }

    return res;
  }

  public IEnumerable<char> ReadRight((int x, int y) start)
  {
    List<char> res = [];
    while (!IsOutOfBounds(start))
    {
      res.Add(Lines[start.y][start.x]);
      start = (start.x + 1, start.y);
    }

    return res;
  }

  public IEnumerable<char> ReadLeft((int x, int y) start)
  {
    List<char> res = [];
    while (!IsOutOfBounds(start))
    {
      res.Add(Lines[start.y][start.x]);
      start = (start.x - 1, start.y);
    }

    return res;
  }

  public IEnumerable<char> ReadUpRight((int x, int y) start)
  {
    List<char> res = [];
    while (!IsOutOfBounds(start))
    {
      res.Add(Lines[start.y][start.x]);
      start = (start.x + 1, start.y - 1);
    }

    return res;
  }

  public IEnumerable<char> ReadUpLeft((int x, int y) start)
  {
    List<char> res = [];
    while (!IsOutOfBounds(start))
    {
      res.Add(Lines[start.y][start.x]);
      start = (start.x - 1, start.y - 1);
    }

    return res;
  }

  public IEnumerable<char> ReadDownRight((int x, int y) start)
  {
    List<char> res = [];
    while (!IsOutOfBounds(start))
    {
      res.Add(Lines[start.y][start.x]);
      start = (start.x + 1, start.y + 1);
    }

    return res;
  }

  public IEnumerable<char> ReadDownLeft((int x, int y) start)
  {
    List<char> res = [];
    while (!IsOutOfBounds(start))
    {
      res.Add(Lines[start.y][start.x]);
      start = (start.x - 1, start.y + 1);
    }

    return res;
  }

  public (int x, int y)[] FindChars(char c)
  {
    List<(int x, int y)> result = [];
    for (int y = 0; y < lines.Length; y++)
    {
      for (int x = 0; x < lines[y].Length; x++)
      {
        if (lines[y][x] == c)
        {
          result.Add((x, y));
        }
      }
    }

    return result.ToArray();
  }

  public Grid DeepCopy()
  {
    return new Grid(lines.Select(line => $"{line}").ToArray());
  }

  public void PrintGrid()
  {
    foreach (var line in Lines)
    {
      Console.WriteLine(line);
    }
  }

  public static int CountTurns((int x, int y)[] positions)
  {
    var direction = -1;
    var result = 0;
    for (var i = 0; i < positions.Length - 1; i++)
    {
      if (positions[i].x == positions[i + 1].x + 1)
      {
        // We went right. Set dir to 0.
        if (direction != 0)
        {
          result += 1;
        }

        direction = 0;
        continue;
      }

      if (positions[i].x == positions[i + 1].x - 1)
      {
        // We went left. Set dir to 1.
        if (direction != 1)
        {
          result += 1;
        }

        direction = 1;
        continue;
      }

      if (positions[i].y == positions[i + 1].y + 1)
      {
        // We went down. Set dir to 2.
        if (direction != 2)
        {
          result += 1;
        }

        direction = 2;
        continue;
      }

      if (positions[i].y == positions[i + 1].y - 1)
      {
        // We went up. Set dir to 3.
        if (direction != 3)
        {
          result += 1;
        }

        direction = 3;
      }
    }

    return result;
  }

  /// <summary>
  /// Get all coordinates of floor tiles that are a corner, i.e. have two or more adjacent walls that are not opposing.
  /// </summary>
  /// <param name="floorTile">The character that would indicate a floor.</param>
  /// <param name="wallTile">The character that would indicate a wall.</param>
  /// <returns></returns>
  public (int x, int y)[] GetCorners(char floorTile = '.', char wallTile = '#')
  {
    var floors = FindChars(floorTile);
    var corners = new List<(int x, int y)>();
    foreach (var floor in floors)
    {
      var above = Above(floor);
      var right = RightOf(floor);
      var under = Under(floor);
      var left = LeftOf(floor);
      if (above == wallTile && under == wallTile || right == wallTile && left == wallTile)
      {
        corners.Add(floor);
      }
    }
    return corners.ToArray();
  }

  public int Dijkstra((int x, int y ) start, (int x, int y) end, char floorTile = '.', char wallTile = '#')
  {
    HashSet<(int x, int y, int dx, int dy)> seen = [
      // TODO assumes facing east
      (start.x, start.y, 1, 0)
    ];
    
    PriorityQueue<(int x, int y, int dx, int dy), int> q = new();
    
    q.Enqueue((start.x, start.y, 1, 0), 0);
    
    while (q.TryDequeue(out var current, out var cost))
    {
      if (current.x == end.x && current.y == end.y)
      {
        return cost;
      }
      // Straight
      if (!IsOutOfBounds((current.x + current.dx, current.y + current.dy)) &&
          GetCharAt((current.x + current.dx, current.y + current.dy)) == floorTile &&
          seen.Add((current.x + current.dx, current.y + current.dy, current.dx, current.dy)))
      {
        q.Enqueue((current.x + current.dx, current.y + current.dy, current.dx, current.dy), cost + 1);
      }

      if (seen.Add((current.x, current.y, current.dy, current.dx)))
      {
        q.Enqueue((current.x, current.y, current.dy, current.dx), cost + 1000);
      }

      if (seen.Add((current.x, current.y, current.dy * -1, current.dx * -1)))
      {
        q.Enqueue((current.x, current.y, current.dy * -1, current.dx * -1), cost + 1000);
      }
    }

    return -1;
  }
}
