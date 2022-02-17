using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2015._20;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var input = int.Parse(Input[0]);

        var house = int.Parse(File.ReadAllText("2015.20.1.result"));

        while (Sigma(house) * 11 < input)
        {
            house++;
        }

        return house.ToString();
    }

    private static int Sigma(int n)
    {
        var sum = 0;

        for (var i = 1; i <= 50 && i <= n; i++)
        {
            if (n % i == 0)
            {
                sum += n / i;
            }
        }

        return sum;
    }
}