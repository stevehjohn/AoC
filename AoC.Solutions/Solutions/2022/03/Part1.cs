using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2022._03;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var sum = 0;

        foreach (var line in Input)
        {
            var compartments = new[] { line[..(line.Length / 2)], line[(line.Length / 2)..] };

            var common = compartments[0].First(i => compartments[1].Contains(i));

            sum += common & 0b0001_1111;

            if ((common & 0b0010_0000) == 0)
            {
                sum += 26;
            }
        }

        return sum.ToString();
    }
}