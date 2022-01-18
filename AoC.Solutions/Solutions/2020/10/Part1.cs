using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2020._10;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseData();

        var differences = GetDifferences();

        return (differences.Count(d => d == 1) * differences.Count(d => d == 3)).ToString();
    }
}