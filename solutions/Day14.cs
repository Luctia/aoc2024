using System.Text;
using System.Text.RegularExpressions;

namespace aoc2024.solutions;

public partial class Day14 : Day
{
  public override void Part1()
  {
    (int width, int height) boardSize = (101, 103);
    var lines = GetInputLines();
    var matchedLines = lines.Select(l => RobotRegex().Matches(l));
    List<(int x, int y)> robotLocations =
      matchedLines.Select(m => (int.Parse(m[0].Groups[1].Value), int.Parse(m[0].Groups[2].Value))).ToList();
    List<(int x, int y)> robotVelocities =
      matchedLines.Select(m => (int.Parse(m[0].Groups[3].Value), int.Parse(m[0].Groups[4].Value))).ToList();
    robotLocations = PerformIterations(robotLocations, robotVelocities, boardSize, 100);

    List<(int x, int y)> topHalf = [];
    List<(int x, int y)> bottomHalf = [];
    foreach (var robotLocation in robotLocations)
    {
      if (robotLocation.y < boardSize.height / 2)
      {
        topHalf.Add(robotLocation);
      }
      else if (robotLocation.y > boardSize.height / 2)
      {
        bottomHalf.Add(robotLocation);
      }
    }

    List<(int x, int y)> topLeft = [];
    List<(int x, int y)> topRight = [];
    List<(int x, int y)> bottomLeft = [];
    List<(int x, int y)> bottomRight = [];
    foreach (var robotLocation in topHalf)
    {
      if (robotLocation.x < boardSize.width / 2)
      {
        topLeft.Add(robotLocation);
      }
      else if (robotLocation.x > boardSize.width / 2)
      {
        topRight.Add(robotLocation);
      }
    }

    foreach (var robotLocation in bottomHalf)
    {
      if (robotLocation.x < boardSize.width / 2)
      {
        bottomLeft.Add(robotLocation);
      }
      else if (robotLocation.x > boardSize.width / 2)
      {
        bottomRight.Add(robotLocation);
      }
    }

    Answer(topLeft.Count * topRight.Count * bottomLeft.Count * bottomRight.Count);
  }

  public override void Part2()
  {
    (int width, int height) boardSize = (101, 103);
    var matchedLines = GetInputLines().Select(l => RobotRegex().Matches(l));
    List<(int x, int y)> robotLocations =
      matchedLines.Select(m => (int.Parse(m[0].Groups[1].Value), int.Parse(m[0].Groups[2].Value))).ToList();
    List<(int x, int y)> robotVelocities =
      matchedLines.Select(m => (int.Parse(m[0].Groups[3].Value), int.Parse(m[0].Groups[4].Value))).ToList();
    int i = 1;
    while (true)
    {
      robotLocations = PerformIterations(robotLocations, robotVelocities, boardSize, 1);
      var lines = FormatBoard(robotLocations, boardSize);
      if (lines.Any(l => l.Contains("######")))
      {
        foreach (var line in lines)
        {
          Console.WriteLine(line);
        }

        Console.WriteLine(i);
        Console.ReadLine();
      }

      i++;
    }
  }

  private static List<(int x, int y)> PerformIterations(List<(int x, int y)> robotLocations,
    List<(int x, int y)> robotVelocities, (int width, int height) boardSize, int iterations)
  {
    for (int i = 0; i < robotLocations.Count; i++)
    {
      robotLocations[i] = (
        ((robotLocations[i].x + iterations * robotVelocities[i].x) % boardSize.width + boardSize.width) %
        boardSize.width,
        ((robotLocations[i].y + iterations * robotVelocities[i].y) % boardSize.height + boardSize.height) %
        boardSize.height);
    }

    return robotLocations;
  }

  private static string[] FormatBoard(List<(int x, int y)> robotLocations, (int width, int height) boardSize)
  {
    List<string> lines = [];
    for (int i = 0; i < boardSize.height; i++)
    {
      var line = new StringBuilder();
      for (int j = 0; j < boardSize.width; j++)
      {
        if (robotLocations.Contains((j, i)))
        {
          line.Append('#');
        }
        else
        {
          line.Append('.');
        }
      }

      lines.Add(line.ToString());
    }

    return lines.ToArray();
  }

  [GeneratedRegex(@"^p=(-?\d+),(-?\d+) v=(-?\d+),(-?\d+)$")]
  private static partial Regex RobotRegex();
}