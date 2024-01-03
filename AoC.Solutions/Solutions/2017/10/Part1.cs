using AoC.Solutions.Solutions._2017.Common;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2017._10;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var lengths = Input[0].Split(',', StringSplitOptions.TrimEntries).Select(int.Parse).ToArray();

        var skipLength = 0;

        var position = 0;
        
        var data = new int[256];

        for (var i = 0; i < 256; i++)
        {
            data[i] = i;
        }

        KnotHash.RunRound(data, lengths, ref position, ref skipLength);

        return (data[0] * data[1]).ToString();
    }
}