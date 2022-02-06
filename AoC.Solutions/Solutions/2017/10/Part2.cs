using System.Text;
using AoC.Solutions.Exceptions;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2017._10;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var lengthsList = Input[0].Select(c => (int) c).ToList();

        lengthsList.AddRange(new[] { 17, 31, 73, 47, 23 });

        var lengths = lengthsList.ToArray();

        var skipLength = 0;

        var position = 0;

        var data = new int[256];

        for (var i = 0; i < 256; i++)
        {
            data[i] = i;
        }

        for (var i = 0; i < 64; i++)
        {
            data = RunRound(data, lengths, ref position, ref skipLength);
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