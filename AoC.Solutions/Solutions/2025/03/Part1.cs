using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2025._03;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var joltage = 0;

        foreach (var line in Input)
        {
            var high = 0;

            for (var l = 0; l < line.Length - 1; l++)
            {
                for (var r = l + 1; r < line.Length; r++)
                {
                    var sum = (line[l] - '0') * 10 + (line[r] - '0');

                    if (sum > high)
                    {
                        high = sum;
                    }
                }
            }

            joltage += high;
        }
        
        return joltage.ToString();
    }
}