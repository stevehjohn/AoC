using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2015._08;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var originalLength = 0;

        var newLength = 0;

        foreach (var line in Input)
        {
            originalLength += line.Length;

            newLength += line.Length + 2 + line.Count(c => c is '\\' or '\"');
        }

        return (newLength - originalLength).ToString();
    }
}