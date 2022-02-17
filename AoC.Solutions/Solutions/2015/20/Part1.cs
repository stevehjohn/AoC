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

        File.WriteAllText("2015.20.1.result", $"{house}");

        return house.ToString();
    }

    private static int Sigma(int n)
    {
        var sum = 0;

        for (var i = 1; i <= Math.Sqrt(n); i++)
        {
            if (n % i == 0)
            {
                sum += i;

                if (n / i != i)
                {
                    sum += n / i;
                }
            }
        }

        return sum;
    }
}