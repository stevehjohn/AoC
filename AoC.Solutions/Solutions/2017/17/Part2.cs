using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2017._17;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var steps = int.Parse(Input[0]);

        var next = 0;

        var target = 0;

        for (var i = 1; i < 50_000_000; i++)
        {
            next = (steps + next) % i + 1;

            if (next == 1)
            {
                target = i;
            }
        }

        return target.ToString();
    }
}