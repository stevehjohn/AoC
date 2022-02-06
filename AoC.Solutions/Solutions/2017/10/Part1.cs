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

        var data = RunRound(lengths, ref position, ref skipLength);

        return (data[0] * data[1]).ToString();
    }
}