namespace aoc2024.solutions;

public class Day01 : Day
{
  public override void Part1()
  {
    var lines = GetInputLines();
    var leftList = lines.Select(line => int.Parse(line.Split("   ")[0])).OrderBy(number => number).ToList();
    var rightList = lines.Select(line => int.Parse(line.Split("   ")[1])).OrderBy(number => number).ToList();
    var totalDistance = 0;
    for (var i = 0; i < leftList.Count; i++)
    {
      totalDistance += Math.Abs(leftList[i] - rightList[i]);
    }

    Answer(totalDistance);
  }

  public override void Part2()
  {
    var lines = GetInputLines();
    var leftList = lines.Select(line => int.Parse(line.Split("   ")[0])).OrderBy(number => number).ToList();
    var rightList = lines.Select(line => int.Parse(line.Split("   ")[1])).OrderBy(number => number).ToList();
    var distinctFromLeft = leftList.Distinct().ToList();
    var similarityScore = distinctFromLeft.Sum(num => num * rightList.Count(i => i == num));
    Answer(similarityScore);
  }
}