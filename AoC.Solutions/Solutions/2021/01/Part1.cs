using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2021._01;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var increases = 0;

        var previous = int.Parse(Input[0]);

        foreach (var line in Input.Skip(1))
        {
            var value = int.Parse(line);

            if (value > previous)
            {
                increases++;
            }

            previous = value;
        }

        return increases.ToString();
    }
}