namespace aoc2024.solutions;

public class Day07 : Day
{
  public override void Part1()
  {
    var lines = GetInputLines();
    Answer(lines
      .Select(l =>
      {
        var result = long.Parse(l[..l.IndexOf(':')]);
        var components = l.Split(" ")[1..].Select(long.Parse).ToArray();
        return (result, components);
      })
      .Where(line => CanBeTrue(line.result, line.components[0], line.components[1..]))
      .Select(r => r.result)
      .Aggregate(0L, (a, b) => a + b));
  }

  public override void Part2()
  {
    var lines = GetInputLines();
    Answer(lines
      .Select(l =>
      {
        var result = long.Parse(l[..l.IndexOf(':')]);
        var components = l.Split(" ")[1..].Select(long.Parse).ToArray();
        return (result, components);
      })
      .Where(line => CanBeTrue(line.result, line.components[0], line.components[1..], true))
      .Select(r => r.result)
      .Aggregate(0L, (a, b) => a + b));
  }

  private static bool CanBeTrue(long res, long current, long[] rest, bool withConcat = false)
  {
    if (rest.Length == 0 && res == current)
    {
      return true;
    }

    if (current > res || rest.Length == 0)
    {
      return false;
    }

    var concatWorks = false;
    if (withConcat)
    {
      var concatted = long.Parse(current.ToString() + rest[0]);
      concatWorks = CanBeTrue(res, concatted, rest[1..], withConcat);
    }

    return CanBeTrue(res, rest[0] * current, rest[1..], withConcat) ||
           CanBeTrue(res, rest[0] + current, rest[1..], withConcat) ||
           concatWorks;
  }
}