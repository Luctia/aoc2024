namespace aoc2024.solutions;

public class Day17 : Day
{
  public override void Part1()
  {
    var input = GetInputLines();
    var registerA = long.Parse(input[0][12..]);
    var registerB = long.Parse(input[1][12..]);
    var registerC = long.Parse(input[2][12..]);
    var instructions = input.Last()[9..].Split(',').Select(short.Parse).ToArray();
    var computer = new Computer(instructions, [registerA, registerB, registerC]);
    computer.Run();
    Answer(string.Join(',', computer.Output));
  }

  public override void Part2()
  {
    var input = GetInputLines();
    var registerB = long.Parse(input[1][12..]);
    var registerC = long.Parse(input[2][12..]);
    var instructions = input.Last()[9..].Split(',').Select(short.Parse).ToArray();
    int a = 1;
    while (true)
    {
      if (TryA(a))
      {
        Answer(a);
        return;
      }

      a++;
    }

    bool TryA(int a)
    {
      var computer = new Computer(instructions, [a, registerB, registerC]);
      computer.Run();
      if (computer.Output.Count != instructions.Length)
      {
        return false;
      }
      for (int i = 0; i < instructions.Length; i++)
      {
        if (instructions[i] != computer.Output[i])
        {
          return false;
        }
      }
      return true;
    }
  }

  private class Computer
  {
    short[] _instructions;
    long _instructionPolonger;
    private long[] _registersABC;
    public List<long> Output { get; }

    public Computer(short[] instructions, long[] registers)
    {
      _instructions = instructions;
      _instructionPolonger = 0;
      _registersABC = registers;
      Output = [];
    }

    public void Run()
    {
      short opCode;
      long literalOperand;
      long comboOperand;
      while (_instructionPolonger < _instructions.Length - 1)
      {
        opCode = _instructions[_instructionPolonger];
        literalOperand = _instructions[_instructionPolonger + 1];
        comboOperand = literalOperand < 4 ? literalOperand : _registersABC[literalOperand - 4];
        switch (opCode)
        {
          case 0:
            _registersABC[0] /= (long)Math.Pow(2, comboOperand);
            _instructionPolonger += 2;
            break;
          case 1:
            _registersABC[1] ^= literalOperand;
            _instructionPolonger += 2;
            break;
          case 2:
            _registersABC[1] = comboOperand % 8;
            _instructionPolonger += 2;
            break;
          case 3:
            _instructionPolonger = _registersABC[0] == 0 ? _instructionPolonger + 2 : literalOperand;
            break;
          case 4:
            _registersABC[1] ^= _registersABC[2];
            _instructionPolonger += 2;
            break;
          case 5:
            Output.Add(comboOperand % 8);
            _instructionPolonger += 2;
            break;
          case 6:
            _registersABC[1] = _registersABC[0] / (long)Math.Pow(2, comboOperand);
            _instructionPolonger += 2;
            break;
          case 7:
            _registersABC[2] = _registersABC[0] / (long)Math.Pow(2, comboOperand);
            _instructionPolonger += 2;
            break;
        }
      }
    }
  }
}