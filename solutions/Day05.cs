namespace aoc2024.solutions;

public class Day05 : Day
{
  public override void Part1()
  {
    var lines = GetInputLines();
    var ruleCount = lines.Count(l => l.Contains('|'));
    var rules = new (int, int)[ruleCount];
    for (var i = 0; i < ruleCount; i++)
    {
      rules[i] = (int.Parse(lines[i].Split('|')[0]), int.Parse(lines[i].Split('|')[1]));
    }

    var sum = 0;
    foreach (var line in lines[(ruleCount + 1)..])
    {
      var sequence = line.Split(',').Select(int.Parse).ToList();
      if (IsOrderedCorrectly(sequence, rules))
      {
        sum += sequence[(sequence.Count - 1) / 2];
      }
    }

    Answer(sum);
  }

  public override void Part2()
  {
    var lines = GetInputLines();
    var ruleCount = lines.Count(l => l.Contains('|'));
    var rules = new (int, int)[ruleCount];
    for (var i = 0; i < ruleCount; i++)
    {
      rules[i] = (int.Parse(lines[i].Split('|')[0]), int.Parse(lines[i].Split('|')[1]));
    }

    var sequences = lines[(ruleCount + 1)..].Select(l => l.Split(',').Select(int.Parse).ToList()).ToList();
    List<List<int>> orderedSequences = [];

    var sum = 0;
    foreach (var seq in sequences)
    {
      if (!IsOrderedCorrectly(seq, rules))
      {
        orderedSequences.Add(OrderSequence(seq, rules));
      }
    }

    foreach (var seq in orderedSequences)
    {
      sum += seq[(seq.Count - 1) / 2];
    }

    Answer(sum);
  }

  private bool IsOrderedCorrectly(List<int> sequence, (int, int)[] rules)
  {
    foreach (var i in rules)
    {
      if (sequence.Contains(i.Item1) && sequence.Contains(i.Item2) &&
          sequence.IndexOf(i.Item1) > sequence.IndexOf(i.Item2))
      {
        return false;
      }
    }

    return true;
  }

  private static List<int> OrderSequence(List<int> sequence, (int, int)[] rules)
  {
    bool pass;
    while (true)
    {
      pass = true;
      foreach (var i in rules)
      {
        if (sequence.Contains(i.Item1) && sequence.Contains(i.Item2))
        {
          var index1 = sequence.IndexOf(i.Item1);
          var index2 = sequence.IndexOf(i.Item2);
          if (index1 > index2)
          {
            sequence[index1] = i.Item2;
            sequence[index2] = i.Item1;
            pass = false;
          }
        }
      }

      if (pass)
      {
        return sequence;
      }
    }
  }
}