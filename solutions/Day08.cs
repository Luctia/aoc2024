using aoc2024.helpers;

namespace aoc2024.solutions;

public class Day08 : Day
{
    public override void Part1()
    {
        var grid = new Grid(GetInputLines());
        var antennas = grid.GetAllSquares().Where(t => t.Item3 != '.').ToList();
        var groups = antennas.GroupBy(t => t.Item3).ToList();
        HashSet<(int, int)> covered = [];
        foreach (var group in groups)
        {
            var g = group.ToList();
            for (int i = 0; i < g.Count; i++)
            {
                for (int j = i + 1; j < g.Count; j++)
                {
                    var g1 = g[i];
                    var g2 = g[j];
                    var nextPoints = GetNextVectorPoints((g1.Item1, g1.Item2), (g2.Item1, g2.Item2), grid);
                    covered.UnionWith(nextPoints.Select(x => (x.Item1, x.Item2)));
                }
            }
        }
        Answer(covered.Count);
    }

    public override void Part2()
    {
        var grid = new Grid(GetInputLines());
        var antennas = grid.GetAllSquares().Where(t => t.Item3 != '.').ToList();
        var groups = antennas.GroupBy(t => t.Item3).ToList();
        HashSet<(int, int)> covered = [];
        foreach (var group in groups)
        {
            var g = group.ToList();
            for (int i = 0; i < g.Count; i++)
            {
                for (int j = i + 1; j < g.Count; j++)
                {
                    var g1 = g[i];
                    var g2 = g[j];
                    var nextPoints = GetAllNextVectorPoints((g1.Item1, g1.Item2), (g2.Item1, g2.Item2), grid);
                    covered.UnionWith(nextPoints.Select(x => (x.Item1, x.Item2)));
                }
            }
        }
        Answer(covered.Count);
    }

    private HashSet<(int, int)> GetNextVectorPoints((int x, int y) pointA, (int x, int y) pointB, Grid grid)
    {
        var points = new HashSet<(int, int)>();
        
        points.Add((pointA.x + (pointA.x - pointB.x), pointA.y + (pointA.y - pointB.y)));
        points.Add((pointB.x + (pointB.x - pointA.x), pointB.y + (pointB.y - pointA.y)));
        
        return points.Where(p => !grid.IsOutOfBounds(p)).ToHashSet();
    }

    private HashSet<(int, int)> GetAllNextVectorPoints((int x, int y) pointA, (int x, int y) pointB, Grid grid)
    {
        var points = new HashSet<(int, int)>();

        var point = pointA;
        var diff = (pointA.x - pointB.x, pointA.y - pointB.y);
        while (!grid.IsOutOfBounds(point))
        {
            points.Add(point);
            point = (point.x + diff.Item1, point.y + diff.Item2);
        }
        point = pointA;
        while (!grid.IsOutOfBounds(point))
        {
            points.Add(point);
            point = (point.x - diff.Item1, point.y - diff.Item2);
        }
        return points;
    }
}