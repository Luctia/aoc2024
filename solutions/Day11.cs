using System.Numerics;

namespace aoc2024.solutions;

public class Day11 : Day
{
    private readonly Dictionary<BigInteger, BigInteger[]> _splits = new();
    
    public override void Part1()
    {
        var stones = GetInput().Split(" ").Select(BigInteger.Parse).ToList();
        Answer(CountStonesAfterBlinks(stones, 25));
    }

    public override void Part2()
    {
        var stones = GetInput().Split(" ").Select(BigInteger.Parse).ToList();
        Answer(CountStonesAfterBlinks(stones, 75));
    }

    private BigInteger CountStonesAfterBlinks(List<BigInteger> stones, int blinks)
    {
        var stonesMap = stones
            .ToDictionary<BigInteger, BigInteger, BigInteger>(stone => stone, stone => stones.Count(s => s == stone));
        for (var i = 0; i < blinks; i++)
        {
            Dictionary<BigInteger, BigInteger> newStones = [];
            foreach (var stoneType in stonesMap.Keys)
            {
                if (stoneType == 0)
                {
                    var exists1 = newStones.TryGetValue(1, out var existing1);
                    newStones[1] = stonesMap[stoneType] + (exists1 ? existing1 : 0);
                    continue;
                }

                var split = GetSplitInteger(stoneType);
                if (split is not null)
                {
                    var newStoneType1 = split[0];
                    var newStoneType2 = split[1];
                    newStones[newStoneType1] = stonesMap[stoneType] + newStones.GetValueOrDefault(newStoneType1, 0);
                    newStones[newStoneType2] = stonesMap[stoneType] + newStones.GetValueOrDefault(newStoneType2, 0);
                    continue;
                }

                var multiplied = BigInteger.Multiply(stoneType, 2024);
                newStones[multiplied] = stonesMap[stoneType] + newStones.GetValueOrDefault(multiplied, 0);
            }
            stonesMap = newStones;
        }

        return stonesMap.Aggregate(new BigInteger(0), (current, pair) => current + pair.Value);
    }

    private BigInteger[]? GetSplitInteger(BigInteger input)
    {
        if (_splits.TryGetValue(input, out var integer))
        {
            return integer;
        }
        var str = input.ToString();
        if (str.Length % 2 == 0)
        {
            BigInteger[] res = [
                BigInteger.Parse(str[..(str.Length / 2)]),
                BigInteger.Parse(str[(str.Length / 2)..])
            ];
            _splits.Add(input, res);
            return res;
        }

        return null;
    }
}