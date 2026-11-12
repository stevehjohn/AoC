using AoC.Solutions.Extensions;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._16;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var input = Input[0];
        
        var inputLength = input.Length;
        
        var totalLength = inputLength * 10_000;

        var offset = int.FastParse(input.AsSpan(0, 7));

        if (offset < totalLength / 2)
        {
            throw new InvalidOperationException("Suffix-sum optimisation requires the offset to be in the second half.");
        }

        var length = totalLength - offset;
        
        var data = new byte[length];

        for (var i = 0; i < length; i++)
        {
            data[i] = (byte) (input[(offset + i) % inputLength] - '0');
        }

        for (var phase = 0; phase < 100; phase++)
        {
            var sum = 0;

            for (var i = length - 1; i >= 0; i--)
            {
                sum += data[i];
                
                sum %= 10;

                data[i] = (byte) sum;
            }
        }

        return string.Create(8, data, static (result, source) =>
        {
            for (var i = 0; i < 8; i++)
            {
                result[i] = (char) ('0' + source[i]);
            }
        });
    }
}