using System.Numerics;

namespace aoc2024.solutions;

public class Day09 : Day
{
  public override void Part1()
  {
    var fs = ConstructFileSystem();
    CompressWithFragmentation(fs);
    Answer(Checksum(fs));
  }

  public override void Part2()
  {
    var fs = ConstructFileSystem();
    CompressWithoutFragmentation(fs);
    Answer(Checksum(fs));
  }

  private short[] ConstructFileSystem()
  {
    var input = GetInput();
    var fsList = new List<short>();
    var atFile = true;
    short id = 0;
    foreach (var c in input)
    {
      if (atFile)
      {
        var num = short.Parse(c.ToString());
        fsList.AddRange(Enumerable.Repeat(id, num));
        id++;
        atFile = false;
      }
      else
      {
        var num = short.Parse(c.ToString());
        fsList.AddRange(Enumerable.Repeat((short)-1, num));
        atFile = true;
      }
    }

    return fsList.ToArray();
  }

  private static void CompressWithFragmentation(short[] fs)
  {
    var pointer = fs.Length - 1;
    while (pointer >= 0)
    {
      if (fs[pointer] >= 0)
      {
        for (int searchPointer = 0; searchPointer < pointer; searchPointer++)
        {
          if (fs[searchPointer] == -1)
          {
            fs[searchPointer] = fs[pointer];
            fs[pointer] = -1;
            break;
          }
        }
      }
      pointer--;
    }
  }

  private static void CompressWithoutFragmentation(short[] fs)
  {
    var pointer = fs.Length - 1;
    while (pointer >= 0)
    {
      var lastFile = FindLastFile(fs, pointer);
      var firstEmptySpace = FindFirstEmptySpace(fs, lastFile.length, lastFile.index);
      if (firstEmptySpace >= 0)
      {
        Array.Fill(fs, fs[lastFile.index], firstEmptySpace, lastFile.length);
        Array.Fill(fs, (short) -1, lastFile.index, lastFile.length);
      }

      pointer = lastFile.index - 1;
    }
  }
  
  private static (int index, int length) FindLastFile(short[] fs, int startIndex)
  {
    for (int i = startIndex; i >= 0; i--)
    {
      var valueAtI = fs[i];
      if (valueAtI > -1)
      {
        var length = 0;
        while (i >= 0 && fs[i] == valueAtI)
        {
          length++;
          i--;
        }
        return (i + 1, length);
      }
    }
    return (-1, -1);
  }

  private static int FindFirstEmptySpace(short[] fs, int minimumSize, int maxIndex)
  {
    for (int i = 0; i < maxIndex; i++)
    {
      if (fs[i] == -1)
      {
        if (minimumSize == 1)
        {
          return i;
        }
        var size = 1;
        i++;
        while (i < fs.Length && fs[i] == -1)
        {
          size++;
          if (size == minimumSize)
          {
            return i - size + 1;
          }
          i++;
        }
      }
    }

    return -1;
  }

  private static BigInteger Checksum(short[] fs)
  {
    var i = 0;
    BigInteger res = 0;
    foreach (var f in fs)
    {
      if (f == -1)
      {
        i++;
        continue;
      }
      res += f * i;
      i++;
    }

    return res;
  }
}