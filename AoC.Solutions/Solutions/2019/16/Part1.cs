using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._16;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var pattern = new[] { 0, 1, 0, -1 };

        var data = Input[0].Select(c => int.Parse(c.ToString())).ToArray();
        //var data = "69317163492948606335995924319873".Select(c => int.Parse(c.ToString())).ToArray();

        var newData = new int[data.Length];

        for (var p = 0; p < 100; p++)
        {
            for (var o = 0; o < data.Length; o++)
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

                newData[o] = Math.Abs(digit) % 10;
            }

            data = newData;
        }

        return new string(data[..8].Select(d => (char) ('0' + d)).ToArray());
    }
}