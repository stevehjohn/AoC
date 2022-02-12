using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2016._09;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var result = GetDecompressedLength(Input[0]);

        return result.ToString();
    }

    private static long GetDecompressedLength(string input)
    {
        var length = 0L;

        return length;
    }
}