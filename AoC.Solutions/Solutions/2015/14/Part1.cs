using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2015._14;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        for (var i = 0; i < 2503; i++)
        {
            ExecuteSecond();
        }

        return Reindeer.Max(r => r.Distance).ToString();
    }
}