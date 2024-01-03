using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2015._23;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var result = RunProgram(new Dictionary<char, int> { { 'a', 0 }, { 'b', 0 } });

        return result.ToString();
    }
}