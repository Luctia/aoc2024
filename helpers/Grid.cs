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

    public (int, int)[] GetAllPositions(char c)
    { 
        (int, int)[] res = new (int, int)[lines.Aggregate(0, (acc, l) => acc += l.Count(x => x == c))];
        int index = 0;
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
}