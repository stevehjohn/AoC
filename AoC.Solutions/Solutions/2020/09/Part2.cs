using AoC.Solutions.Exceptions;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2020._09;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseData();

        var targetIndex = int.Parse(File.ReadAllText(Part1ResultFile));

        var target = Data[targetIndex];

        for (var i = targetIndex - 1; i >= 0; i--)
        {
            var sum = 0L;

            for (var j = i; j >= 0; j--)
            {
                sum += Data[j];

                if (j == targetIndex)
                {
                    break;
                }

                if (sum == target)
                {
                    var min = long.MaxValue;

                    var max = long.MinValue;

                    for (var k = j; k <= i; k++)
                    {
                        min = Math.Min(min, Data[k]);

                        max = Math.Max(max, Data[k]);
                    }

                    return (min + max).ToString();
                }

                if (sum > target)
                {
                    break;
                }
            }
        }

        throw new PuzzleException("Solution not found.");
    }
}