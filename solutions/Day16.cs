using aoc2024.helpers;

namespace aoc2024.solutions;

public class Day16 : Day
{
  public override void Part1()
  {
    var grid = new Grid(GetInputLines());
    var currentPos = grid.GetGuardPosition('S');
    var end = grid.GetGuardPosition('E');
    grid.SetCharAt(currentPos, '.');
    grid.SetCharAt(end, '.');
    Answer(grid.Dijkstra(currentPos, end));
  }

  public override void Part2()
  {
    throw new NotImplementedException();
  }
}