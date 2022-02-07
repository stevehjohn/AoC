using AoC.Solutions.Solutions._2017.Common;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2017._14;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var bits = 0;

        for (var i = 0; i < 128; i++)
        {
            var rowHash = KnotHash.MakeHash($"{Input[0]}-i");

            bits += CountBits(rowHash);
        }

        return bits.ToString();
    }

    private static int CountBits(string rowHash)
    {
        var bits = 0;

        foreach (var c in rowHash)
        {
            
        }

        return bits;
    }
}