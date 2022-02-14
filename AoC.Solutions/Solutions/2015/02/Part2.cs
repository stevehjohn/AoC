using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2015._02;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var total = 0;

        foreach (var box in Input)
        {
            var sides = box.Split('x').Select(int.Parse).OrderBy(s => s).ToList();

            total += 2 * sides[0] + 2 * sides[1];

            total += sides[0] * sides[1] * sides[2];
        }

        return total.ToString();
    }
}