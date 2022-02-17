using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2015._23;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var result = RunProgram(new Dictionary<char, int> { { 'a', 1 }, { 'b', 0 } });

        return result.ToString();
    }
}