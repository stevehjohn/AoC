using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2017._01;

public abstract class Base : Solution
{
    public override string Description => "Captcha";

    protected int Solve(int offset = 1)
    {
        var sum = 0;

        var data = Input[0];

        var length = data.Length;

        for (var i = 0; i < length; i++)
        {
            var otherIndex = (i + offset) % length;

            if (data[i] == data[otherIndex])
            {
                sum += data[i] - '0';
            }
        }

        return sum;
    }
}