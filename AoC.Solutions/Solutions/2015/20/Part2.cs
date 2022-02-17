using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2015._20;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var input = int.Parse(Input[0]);

        var house = int.Parse(File.ReadAllText("2015.20.1.result")) + 1;

        //while (Sigma(house) * 10 < input)
        while (true)
        {
            var factors = GetFactors(house).TakeLast(50);

            if (factors.Sum() * 11 >= input)
            {
                break;
            }

            house++;
        }

        return house.ToString();
    }

    private static List<int> GetFactors(int n)
    {
        var result = new List<int>();

        for (var i = 1; i <= Math.Sqrt(n); i++)
        {
            if (n % i == 0)
            {
                result.Add(i);

                if (n / i != i)
                {
                    result.Add(n / i);
                }
            }
        }

        return result;
    }
}