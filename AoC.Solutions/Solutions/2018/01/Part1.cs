using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2018._01;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var frequency = 0;

        foreach (var line in Input)
        {
            var sign = line[0] == '-' ? -1 : 1;

            frequency += sign * int.Parse(line[1..]);
        }

        return frequency.ToString();
    }
}