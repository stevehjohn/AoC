using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2015._20;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var input = int.Parse(Input[0]);

        var house = 1;

        while (Sigma(house) * 10 < input)
        {
            house++;
        }

        return house.ToString();
    }

    private static int Sigma(int n)
    {
        var sum = n;

        var maxDivisor = n % 2 == 0 ? n / 2 : n / 3;

        for (var i = maxDivisor; i > 0; i--)
        {
            if (n % i == 0)
            {
                sum += i;
            }
        }

        return sum;
    }
}