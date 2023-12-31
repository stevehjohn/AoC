using AoC.Solutions.Exceptions;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2020._09;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseData();

        var possibleNumbers = new List<HashSet<long>>();

        for (var i = 1; i < Data.Count; i++)
        {
            if (possibleNumbers.Count > 24)
            {
                if (! IsInPossibleNumbers(possibleNumbers, Data[i]))
                {
                    File.WriteAllText(Part1ResultFile, i.ToString());

                    return Data[i].ToString();
                }
            }

            possibleNumbers.Add(CalculateNextPossibleNumbers(i));

            if (possibleNumbers.Count > 25)
            {
                possibleNumbers.RemoveAt(0);
            }
        }

        throw new PuzzleException("Solution not found.");
    }

    private HashSet<long> CalculateNextPossibleNumbers(int index)
    {
        var possibleNumbers = new HashSet<long>();

        for (var i = index - 1; i >= Math.Max(0, index - 24); i--)
        {
            if (Data[index] == Data[i])
            {
                continue;
            }

            possibleNumbers.Add(Data[index] + Data[i]);
        }

        return possibleNumbers;
    }

    private static bool IsInPossibleNumbers(List<HashSet<long>> possibleNumbers, long value)
    {
        foreach (var item in possibleNumbers)
        {
            if (item.Contains(value))
            {
                return true;
            }
        }

        return false;
    }
}