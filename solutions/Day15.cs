using aoc2024.helpers;

namespace aoc2024.solutions;

public class Day15 : Day
{
  public override void Part1()
  {
    var lines = GetInputLines();
    var instructions = lines.Last();
    var grid = new Grid(lines.SkipLast(2).ToArray());
    var robotPosition = grid.GetGuardPosition('@');
    foreach (var instruction in instructions)
    {
      (grid, robotPosition) = PerformInstruction(grid, instruction, robotPosition);
    }

    Answer(grid.FindChars('O').Select(b => b.x + 100 * b.y).Sum());
  }

  public override void Part2()
  {
    var lines = GetInputLines();
    List<string> wideLines = [];
    foreach (var line in lines.SkipLast(2))
    {
      wideLines.Add(line.Select(c => $"{c}{c}").Aggregate((s1, s2) => s1 + s2).Replace("OO", "[]").Replace("@@", "@."));
    }

    var wideLinesArray = wideLines.ToArray();
    var instructions = lines.Last();
    var grid = new Grid(wideLinesArray);
    var robotPosition = grid.GetGuardPosition('@');
    grid.PrintGrid();
    foreach (var instruction in instructions)
    {
      (grid, robotPosition) = PerformInstructionWideGrid(grid, instruction, robotPosition);
    }

    Answer(grid.FindChars('[').Select(b => b.x + 100 * b.y).Sum());
  }

  private static (Grid, (int, int)) PerformInstruction(Grid grid, char instruction, (int x, int y) robotPosition)
  {
    switch (instruction)
    {
      case '>':
      {
        var checkFrom = robotPosition;
        var boxesPushed = 0;
        while (grid.RightOf(checkFrom) != '.')
        {
          if (grid.RightOf(checkFrom) == '#')
          {
            return (grid, robotPosition);
          }

          if (grid.RightOf(checkFrom) == 'O')
          {
            checkFrom = (checkFrom.x + 1, checkFrom.y);
            boxesPushed++;
          }
        }

        grid.SetCharAt(robotPosition, '.');
        grid.SetCharAt((robotPosition.x + 1, robotPosition.y), '@');
        for (var i = 1; i <= boxesPushed; i++)
        {
          grid.SetCharAt((robotPosition.x + 1 + i, robotPosition.y), 'O');
        }

        return (grid, (robotPosition.x + 1, robotPosition.y));
      }
      case 'v':
      {
        var checkFrom = robotPosition;
        var boxesPushed = 0;
        while (grid.Under(checkFrom) != '.')
        {
          if (grid.Under(checkFrom) == '#')
          {
            return (grid, robotPosition);
          }

          if (grid.Under(checkFrom) == 'O')
          {
            checkFrom = (checkFrom.x, checkFrom.y + 1);
            boxesPushed++;
          }
        }

        grid.SetCharAt(robotPosition, '.');
        grid.SetCharAt((robotPosition.x, robotPosition.y + 1), '@');
        for (var i = 1; i <= boxesPushed; i++)
        {
          grid.SetCharAt((robotPosition.x, robotPosition.y + 1 + i), 'O');
        }

        return (grid, (robotPosition.x, robotPosition.y + 1));
      }
      case '<':
      {
        var checkFrom = robotPosition;
        var boxesPushed = 0;
        while (grid.LeftOf(checkFrom) != '.')
        {
          if (grid.LeftOf(checkFrom) == '#')
          {
            return (grid, robotPosition);
          }

          if (grid.LeftOf(checkFrom) == 'O')
          {
            checkFrom = (checkFrom.x - 1, checkFrom.y);
            boxesPushed++;
          }
        }

        grid.SetCharAt(robotPosition, '.');
        grid.SetCharAt((robotPosition.x - 1, robotPosition.y), '@');
        for (var i = 1; i <= boxesPushed; i++)
        {
          grid.SetCharAt((robotPosition.x - 1 - i, robotPosition.y), 'O');
        }

        return (grid, (robotPosition.x - 1, robotPosition.y));
      }
      case '^':
      {
        var checkFrom = robotPosition;
        var boxesPushed = 0;
        while (grid.Above(checkFrom) != '.')
        {
          if (grid.Above(checkFrom) == '#')
          {
            return (grid, robotPosition);
          }

          if (grid.Above(checkFrom) == 'O')
          {
            checkFrom = (checkFrom.x, checkFrom.y - 1);
            boxesPushed++;
          }
        }

        grid.SetCharAt(robotPosition, '.');
        grid.SetCharAt((robotPosition.x, robotPosition.y - 1), '@');
        for (var i = 1; i <= boxesPushed; i++)
        {
          grid.SetCharAt((robotPosition.x, robotPosition.y - 1 - i), 'O');
        }

        return (grid, (robotPosition.x, robotPosition.y - 1));
      }
      default: throw new Exception();
    }
  }

  private static (Grid, (int, int)) PerformInstructionWideGrid(Grid grid, char instruction,
    (int x, int y) robotPosition)
  {
    switch (instruction)
    {
      case '>':
      {
        var checkFrom = robotPosition;
        var boxesPushed = 0;
        while (grid.RightOf(checkFrom) != '.')
        {
          if (grid.RightOf(checkFrom) == '#')
          {
            return (grid, robotPosition);
          }

          if (grid.RightOf(checkFrom) == '[')
          {
            checkFrom = (checkFrom.x + 2, checkFrom.y);
            boxesPushed++;
          }
        }

        grid.SetCharAt(robotPosition, '.');
        grid.SetCharAt((robotPosition.x + 1, robotPosition.y), '@');
        for (var i = 1; i <= boxesPushed; i += 1)
        {
          grid.SetCharAt((robotPosition.x + i * 2, robotPosition.y), '[');
          grid.SetCharAt((robotPosition.x + 1 + i * 2, robotPosition.y), ']');
        }

        return (grid, (robotPosition.x + 1, robotPosition.y));
      }
      case 'v':
      {
        if (grid.Under(robotPosition) == '#')
        {
          return (grid, robotPosition);
        }

        if (grid.Under(robotPosition) == '.')
        {
          grid.SetCharAt(robotPosition, '.');
          grid.SetCharAt((robotPosition.x, robotPosition.y + 1), '@');
          return (grid, (robotPosition.x, robotPosition.y + 1));
        }

        grid = PushWideBoxDown(grid,
          grid.Under(robotPosition) == '['
            ? (robotPosition.x, robotPosition.y + 1)
            : (robotPosition.x - 1, robotPosition.y + 1));
        if (grid.Under(robotPosition) == '.')
        {
          grid.SetCharAt(robotPosition, '.');
          grid.SetCharAt((robotPosition.x, robotPosition.y + 1), '@');
          return (grid, (robotPosition.x, robotPosition.y + 1));
        }

        return (grid, robotPosition);
      }
      case '<':
      {
        var checkFrom = robotPosition;
        var boxesPushed = 0;
        while (grid.LeftOf(checkFrom) != '.')
        {
          if (grid.LeftOf(checkFrom) == '#')
          {
            return (grid, robotPosition);
          }

          if (grid.LeftOf(checkFrom) == ']')
          {
            checkFrom = (checkFrom.x - 2, checkFrom.y);
            boxesPushed++;
          }
        }

        grid.SetCharAt(robotPosition, '.');
        grid.SetCharAt((robotPosition.x - 1, robotPosition.y), '@');
        for (var i = 1; i <= boxesPushed; i++)
        {
          grid.SetCharAt((robotPosition.x - i * 2, robotPosition.y), ']');
          grid.SetCharAt((robotPosition.x - 1 - i * 2, robotPosition.y), '[');
        }

        return (grid, (robotPosition.x - 1, robotPosition.y));
      }
      case '^':
      {
        if (grid.Above(robotPosition) == '#')
        {
          return (grid, robotPosition);
        }

        if (grid.Above(robotPosition) == '.')
        {
          grid.SetCharAt(robotPosition, '.');
          grid.SetCharAt((robotPosition.x, robotPosition.y - 1), '@');
          return (grid, (robotPosition.x, robotPosition.y - 1));
        }

        grid = PushWideBoxUp(grid,
          grid.Above(robotPosition) == '['
            ? (robotPosition.x, robotPosition.y - 1)
            : (robotPosition.x - 1, robotPosition.y - 1));
        if (grid.Above(robotPosition) == '.')
        {
          grid.SetCharAt(robotPosition, '.');
          grid.SetCharAt((robotPosition.x, robotPosition.y - 1), '@');
          return (grid, (robotPosition.x, robotPosition.y - 1));
        }

        return (grid, robotPosition);
      }
      default: throw new Exception();
    }
  }

  private static Grid PushWideBoxDown(Grid grid, (int x, int y) boxLeftPosition)
  {
    // If there's a wall under the box we can't push it. Return original grid.
    if (grid.Under(boxLeftPosition) == '#' || grid.Under(((boxLeftPosition.x + 1), boxLeftPosition.y)) == '#')
    {
      return grid;
    }

    // If there's a box directly under this box, try to push that box down too.
    if (grid.Under(boxLeftPosition) == '[')
    {
      var ogLine = grid.Lines[boxLeftPosition.y + 1];
      grid = PushWideBoxDown(grid, (boxLeftPosition.x, boxLeftPosition.y + 1));
      if (ogLine.Equals(grid.Lines[boxLeftPosition.y + 1]))
      {
        // If the line hasn't changed, the box was not moved. Return the grid.
        return grid;
      }
    }

    var gridBackup = grid.DeepCopy();
    // If there's a box left-under this one...
    if (grid.Under(boxLeftPosition) == ']')
    {
      // Try to push the box left-under up
      var ogLine = grid.Lines[boxLeftPosition.y + 1];
      grid = PushWideBoxDown(grid, (boxLeftPosition.x - 1, boxLeftPosition.y + 1));
      if (ogLine.Equals(grid.Lines[boxLeftPosition.y + 1]))
      {
        // If the line hasn't changed, the box was not moved. Return the grid as it was before the potential box right-under was pushed.
        return grid;
      }
    }

    // If there's a box right-under this one
    if (grid.Under((boxLeftPosition.x + 1, boxLeftPosition.y)) == '[')
    {
      // Try to push the box right-under down
      var ogLine = grid.Lines[boxLeftPosition.y + 1];
      grid = PushWideBoxDown(grid, (boxLeftPosition.x + 1, boxLeftPosition.y + 1));
      if (ogLine.Equals(grid.Lines[boxLeftPosition.y + 1]))
      {
        // If the line hasn't changed, the box was not moved. Return the grid.
        return gridBackup;
      }
    }

    if (grid.Under(boxLeftPosition) == '.' && grid.Under(((boxLeftPosition.x + 1), boxLeftPosition.y)) == '.')
    {
      grid.SetCharAt(boxLeftPosition, '.');
      grid.SetCharAt((boxLeftPosition.x + 1, boxLeftPosition.y), '.');
      grid.SetCharAt((boxLeftPosition.x, boxLeftPosition.y + 1), '[');
      grid.SetCharAt((boxLeftPosition.x + 1, boxLeftPosition.y + 1), ']');
    }

    return grid;
  }

  private static Grid PushWideBoxUp(Grid grid, (int x, int y) boxLeftPosition)
  {
    // If there's a wall above the box we can't push it. Return original grid.
    if (grid.Above(boxLeftPosition) == '#' || grid.Above(((boxLeftPosition.x + 1), boxLeftPosition.y)) == '#')
    {
      return grid;
    }

    // If there's a box directly above this box, try to push that box up too.
    if (grid.Above(boxLeftPosition) == '[')
    {
      var ogLine = grid.Lines[boxLeftPosition.y - 1];
      grid = PushWideBoxUp(grid, (boxLeftPosition.x, boxLeftPosition.y - 1));
      if (ogLine.Equals(grid.Lines[boxLeftPosition.y - 1]))
      {
        // If the line hasn't changed, the box was not moved. Return the grid.
        return grid;
      }
    }

    var gridBackup = grid.DeepCopy();

    // If there's a box left-above this one...
    if (grid.Above(boxLeftPosition) == ']')
    {
      // Try to push the box left-above up
      var ogLine = grid.Lines[boxLeftPosition.y - 1];
      grid = PushWideBoxUp(grid, (boxLeftPosition.x - 1, boxLeftPosition.y - 1));
      if (ogLine.Equals(grid.Lines[boxLeftPosition.y - 1]))
      {
        // If the line hasn't changed, the box was not moved. Return the grid as it was before the potential box right-above was pushed.
        return grid;
      }
    }

    // And also a box right-above this one...
    if (grid.Above((boxLeftPosition.x + 1, boxLeftPosition.y)) == '[')
    {
      // Try to push the box right-above up
      var ogLine = grid.Lines[boxLeftPosition.y - 1];
      grid = PushWideBoxUp(grid, (boxLeftPosition.x + 1, boxLeftPosition.y - 1));
      if (ogLine.Equals(grid.Lines[boxLeftPosition.y - 1]))
      {
        // If the line hasn't changed, the box was not moved. Return the grid.
        return gridBackup;
      }
    }

    if (grid.Above(boxLeftPosition) == '.' && grid.Above(((boxLeftPosition.x + 1), boxLeftPosition.y)) == '.')
    {
      grid.SetCharAt(boxLeftPosition, '.');
      grid.SetCharAt((boxLeftPosition.x + 1, boxLeftPosition.y), '.');
      grid.SetCharAt((boxLeftPosition.x, boxLeftPosition.y - 1), '[');
      grid.SetCharAt((boxLeftPosition.x + 1, boxLeftPosition.y - 1), ']');
    }

    return grid;
  }
}