using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2022._01;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        return Calories.OrderByDescending(c => c).Take(3).Sum().ToString();
    }
}