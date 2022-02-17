using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2015._20;

public abstract class Base : Solution
{
    public override string Description => "Infinite elves, infinite houses";

    protected static int Sigma(int n)
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