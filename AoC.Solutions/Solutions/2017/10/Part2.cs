using System.Text;
using AoC.Solutions.Exceptions;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2017._10;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var lengths = $"{Input[0]},17,31,73,47,23".Select(c => (int) c).ToArray();

        var skipLength = 0;

        var position = 0;

        int[] data = null;

        for (var i = 0; i < 64; i++)
        {
            data = RunRound(lengths, ref position, ref skipLength);
        }

        if (data == null)
        {
            throw new PuzzleException("This shouldn't happen.");
        }

        var hash = new int[16];

        for (var i = 0; i < 16; i++)
        {
            for (var x = 0; x < 16; x++)
            {
                hash[i] ^= data[i * 16 + x];
            }
        }

        var builder = new StringBuilder();

        foreach (var item in hash)
        {
            builder.Append(item.ToString("X2"));
        }

        var result = builder.ToString().ToLower();

        return result;
    }
}