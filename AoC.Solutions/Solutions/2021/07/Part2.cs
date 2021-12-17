using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2021._07;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var positions = Input[0].Split(',').Select(int.Parse).OrderBy(n => n).ToList();

        var mean = positions.Sum() / positions.Count;

        var fuel = positions.Select(n =>
        {
            var delta = Math.Abs(n - mean);

            return delta * (delta + 1) / 2;
        }).Sum();

        return fuel.ToString();
    }
}