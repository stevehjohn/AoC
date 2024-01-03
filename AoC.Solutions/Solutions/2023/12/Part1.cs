using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._12;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var sum = 0L;

        foreach (var line in Input)
        {
            var data = ParseLine(line);

            sum += GetArrangements(data.Row, data.Groups);
        }
        
        return sum.ToString();
    }
}