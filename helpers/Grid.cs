namespace aoc2024.helpers;

internal class Grid(string[] lines)
{
    public bool IsOutOfBounds((int x, int y) position)
    {
        return position.y < 0 || position.y >= lines.Length || position.x < 0 || position.x >= lines[0].Length;
    }

    public (int, int) GetGuardPosition()
    {
        var y = -1;
        for (var i = 0; i < lines.Length; i++)
        {
            if (lines[i].Contains('^'))
            {
                y = i;
                break;
            }
        }

        var x = 0;
        for (var i = 0; i < lines[y].Length; i++)
        {
            if (lines[y][i] == '^')
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
        return lines[position.y][position.x];
    }

    public void SetCharAt((int x, int y) position, char value)
    {
        lines[position.y] = lines[position.y].Insert(position.x, value.ToString()).Remove(position.x + 1, 1);
    }

    public (int, int)[] GetAllPositions(char c)
    { 
        var res = new (int, int)[lines.Aggregate(0, (acc, l) => acc + l.Count(x => x == c))];
        var index = 0;
        for (var y = 0; y < lines.Length; y++)
        {
            for (int x = 0; x < lines[y].Length; x++)
            {
                if (lines[y][x] == c)
                {
                    res[index] = (y, x);
                    index++;
                }
            }
        }
        return res;
    }

    public char? Above((int x, int y) position)
    {
        if (IsOutOfBounds(position))
        {
            return null;
        }
        return lines[position.y - 1][position.x];
    }

    public char? Under((int x, int y) position)
    {
        if (IsOutOfBounds(position))
        {
            return null;
        }
        return lines[position.y + 1][position.x];
    }

    public char? RightOf((int x, int y) position)
    {
        if (IsOutOfBounds(position))
        {
            return null;
        }
        return lines[position.y][position.x + 1];
    }

    public char? LeftOf((int x, int y) position)
    {
        if (IsOutOfBounds(position))
        {
            return null;
        }
        return lines[position.y][position.x - 1];
    }

    public char? AboveRightOf((int x, int y) position)
    {
        var pos = (position.x + 1, position.y - 1);
        if (IsOutOfBounds(pos))
        {
            return null;
        }

        return lines[pos.Item2][pos.Item1];
    }

    public char? AboveLeftOf((int x, int y) position)
    {
        var pos = (position.x - 1, position.y - 1);
        if (IsOutOfBounds(pos))
        {
            return null;
        }

        return lines[pos.Item2][pos.Item1];
    }

    public char? UnderRightOf((int x, int y) position)
    {
        var pos = (position.x + 1, position.y + 1);
        if (IsOutOfBounds(pos))
        {
            return null;
        }

        return lines[pos.Item2][pos.Item1];
    }

    public char? UnderLeftOf((int x, int y) position)
    {
        var pos = (position.x - 1, position.y + 1);
        if (IsOutOfBounds(pos))
        {
            return null;
        }

        return lines[pos.Item2][pos.Item1];
    }

    public List<(int, int, char)> GetAllSquares()
    {
        List<(int, int, char)> res = new();
        for (int y = 0; y < lines.Length; y++)
        {
            for (int x = 0; x < lines[y].Length; x++)
            {
                res.Add((x, y, lines[y][x]));
            }
        }

        return res;
    }

    public List<(int, int, char)> GetEightAround((int x, int y) coords)
    {
        List<(int, int, char)> res = [
            (coords.x - 1, coords.y - 1, lines[coords.y - 1][coords.x - 1]),
            (coords.x, coords.y - 1, lines[coords.y - 1][coords.x]),
            (coords.x + 1, coords.y - 1, lines[coords.y - 1][coords.x + 1]),
            (coords.x - 1, coords.y, lines[coords.y][coords.x - 1]),
            (coords.x + 1, coords.y, lines[coords.y][coords.x + 1]),
            (coords.x - 1, coords.y + 1, lines[coords.y + 1][coords.x - 1]),
            (coords.x, coords.y + 1, lines[coords.y + 1][coords.x]),
            (coords.x + 1, coords.y + 1, lines[coords.y + 1][coords.x + 1])
        ];
        return res;
    }

    public List<(int, int, char?)> GetFourAround((int x, int y) coords)
    {
        List<(int, int, char?)> res = [
            (coords.x, coords.y - 1, lines[coords.y - 1]?[coords.x] ?? null),
            (coords.x - 1, coords.y, lines[coords.y]?[coords.x - 1] ?? null),
            (coords.x + 1, coords.y, lines[coords.y]?[coords.x + 1] ?? null),
            (coords.x, coords.y + 1, lines[coords.y + 1]?[coords.x] ?? null)
        ];
        return res;
    }

    public IEnumerable<char> ReadUpwards((int x, int y) start)
    {
        List<char> res = [];
        while (!IsOutOfBounds(start))
        {
            res.Add(lines[start.y][start.x]);
            start = (start.x, start.y - 1);
        }

        return res;
    }

    public IEnumerable<char> ReadDownwards((int x, int y) start)
    {
        List<char> res = [];
        while (!IsOutOfBounds(start))
        {
            res.Add(lines[start.y][start.x]);
            start = (start.x, start.y + 1);
        }

        return res;
    }

    public IEnumerable<char> ReadRight((int x, int y) start)
    {
        List<char> res = [];
        while (!IsOutOfBounds(start))
        {
            res.Add(lines[start.y][start.x]);
            start = (start.x + 1, start.y);
        }

        return res;
    }

    public IEnumerable<char> ReadLeft((int x, int y) start)
    {
        List<char> res = [];
        while (!IsOutOfBounds(start))
        {
            res.Add(lines[start.y][start.x]);
            start = (start.x - 1, start.y);
        }

        return res;
    }

    public IEnumerable<char> ReadUpRight((int x, int y) start)
    {
        List<char> res = [];
        while (!IsOutOfBounds(start))
        {
            res.Add(lines[start.y][start.x]);
            start = (start.x + 1, start.y - 1);
        }

        return res;
    }

    public IEnumerable<char> ReadUpLeft((int x, int y) start)
    {
        List<char> res = [];
        while (!IsOutOfBounds(start))
        {
            res.Add(lines[start.y][start.x]);
            start = (start.x - 1, start.y - 1);
        }

        return res;
    }

    public IEnumerable<char> ReadDownRight((int x, int y) start)
    {
        List<char> res = [];
        while (!IsOutOfBounds(start))
        {
            res.Add(lines[start.y][start.x]);
            start = (start.x + 1, start.y + 1);
        }

        return res;
    }

    public IEnumerable<char> ReadDownLeft((int x, int y) start)
    {
        List<char> res = [];
        while (!IsOutOfBounds(start))
        {
            res.Add(lines[start.y][start.x]);
            start = (start.x - 1, start.y + 1);
        }

        return res;
    }
}