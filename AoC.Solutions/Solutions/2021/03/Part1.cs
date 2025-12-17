using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2021._03;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var length = Input[0].Length;

        var bit = 1 << (length - 1);

        var value = 0;

        for (var i = 0; i < length; i++)
        {
            var ones = 0;

            for (var l = 0; l < Input.Length; l++)
            {
                if (Input[l][i] == '1')
                {
                    ones++;

                    if (ones > Input.Length / 2)
                    {
                        value += bit;
                        
                        break;
                    }
                }
            }

            bit >>= 1;
        }

        var mask = (1 << length) - 1;

        return (value * (value ^ mask)).ToString();
    }
}