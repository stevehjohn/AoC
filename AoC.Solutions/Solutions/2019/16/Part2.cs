using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._16;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var pattern = new[] { 0, 1, 0, -1 };

        //var data = string.Concat(Enumerable.Repeat(Input[0], 10_000)).Select(c => byte.Parse(c.ToString())).ToArray();
        var data = string.Concat(Enumerable.Repeat("03036732577212944063491565474664", 10_000)).Select(c => byte.Parse(c.ToString())).ToArray();

        //var offset = int.Parse(Input[0][..7]);
        var offset = 303673;

        for (var p = 0; p < 100; p++)
        {
            Console.WriteLine(p);

            for (var o = offset; o < data.Length; o++)
            {
                var digit = 0;

                var patternIndex = 0;

                for (var i = o; i < data.Length; i++)
                {
                    if ((i + 1) % (o + 1) == 0)
                    {
                        patternIndex++;

                        if (patternIndex > 3)
                        {
                            patternIndex = 0;
                        }
                    }

                    digit += data[i] * pattern[patternIndex];
                }

                data[o] = (byte) (Math.Abs(digit) % 10);
            }
        }

        return new string(data[offset..(offset + 8)].Select(d => (char) ('0' + d)).ToArray());
    }
}