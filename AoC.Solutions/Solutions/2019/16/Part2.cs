using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._16;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var data = string.Concat(Enumerable.Repeat(Input[0], 10_000)).Select(c => byte.Parse(c.ToString())).ToArray();

        var offset = int.Parse(Input[0][..7]);

        for (var p = 0; p < 100; p++)
        {
            var sum = 0;

            int i;

            for (i = offset; i < data.Length; i++)
            {
                sum += data[i];
            }

            for (i = offset; i < data.Length; i++)
            {
                var temp = data[i];

                data[i] = (byte) (sum % 10);

                sum -= temp;
            }
        }

        return new string(data[offset..(offset + 8)].Select(d => (char) ('0' + d)).ToArray());
    }
}