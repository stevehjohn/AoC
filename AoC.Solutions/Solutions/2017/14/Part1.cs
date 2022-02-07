using AoC.Solutions.Extensions;
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
            var rowHash = KnotHash.MakeHash($"{Input[0]}-{i}").ToList();

            bits += rowHash.Sum(c => "0123456789abcdef".IndexOf(c).CountBits());
        }

        return bits.ToString();
    }
}