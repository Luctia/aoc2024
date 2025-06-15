namespace aoc2024.helpers;

internal class Grid(string[] lines)
{
    public string[] Lines { get; set; } = lines;

    public bool IsOutOfBounds((int x, int y) position)
    {
        return position.y < 0 || position.y >= Lines.Length || position.x < 0 || position.x >= Lines[0].Length;
    }

    public (int, int) GetGuardPosition()
    {
        var y = -1;
        for (var i = 0; i < Lines.Length; i++)
        {
            if (Lines[i].Contains('^'))
            {
                y = i;
                break;
            }
        }

        var x = 0;
        for (var i = 0; i < Lines[y].Length; i++)
        {
            if (Lines[y][i] == '^')
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
        List<(int, int, char)> res = [
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

    public HashSet<(int, int, char?)> GetFourAround((int x, int y) coords)
    {
        HashSet<(int, int, char?)> res = [
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
}