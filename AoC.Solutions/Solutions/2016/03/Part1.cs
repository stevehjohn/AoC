using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2016._03;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var count = 0;

        foreach (var line in Input)
        {
            var sides = line.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).OrderBy(s => s).ToArray();

            if (sides[0] + sides[1] > sides[2])
            {
                count++;
            }
        }

        return count.ToString();
    }
}