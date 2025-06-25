using System.Text.RegularExpressions;

namespace aoc2024.solutions;

public class Day19 : Day
{
  public override void Part1()
  {
    var towels = GetInputLines()[0].Split(", ");
    var designs = GetInputLines()[2..];
    var regex = new Regex("^(" + towels.Aggregate("", (s, s1) => s + "|" + s1)[1..] + ")+$");
    Answer(designs.Count(d => regex.IsMatch(d)));
  }

  public override void Part2()
  {
    var towels = GetInputLines()[0].Split(", ").Order().ToArray();
    var regex = new Regex("^(" + towels.Aggregate("", (s, s1) => s + "|" + s1)[1..] + ")+$");
    var designs = GetInputLines()[2..].Where(d => regex.IsMatch(d));
    var res = 0;
    foreach (var design in designs)
    {
      res += GetCombinations(design, towels);
      Console.WriteLine(res);
    }
    Answer(res);
  }
  
  private static int GetCombinations(ReadOnlySpan<char> design, string[] towels)
  {
    // if (design.Length == 0)
    // {
    //   return 1;
    // }
    //
    var result = 0;
    // var firstDesignChar = design[0];
    // foreach (var towel in towels.Where(x => x[0] == firstDesignChar))
    // {
    //   if (towel.Equals(design[..towel.Length]))
    //   {
    //     result += GetCombinations(design[towel.Length..], towels);
    //   } 
    // }
    // // Console.WriteLine($"Found towels {string.Join(", ", candidateTowels)} for design {design.ToString()}");
    return result;
  }
}
