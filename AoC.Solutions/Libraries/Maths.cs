namespace AoC.Solutions.Libraries;

public static class Maths
{
    public static long LowestCommonMultiple(List<long> input)
    {
        if (input.Count == 2)
        {
            var left = input[0];

            var right = input[1];

            return left * right / GreatestCommonFactor(left, right);
        }

        var lowestCommonMultiple = LowestCommonMultiple(input.Take(2).ToList());

        var remaining = input.Skip(2).ToList();

        remaining.Add(lowestCommonMultiple);

        return LowestCommonMultiple(remaining);
    }

    private static long GreatestCommonFactor(long left, long right)
    {
        while (left != right)
        {
            if (left > right)
            {
                left -= right;
            }
            else
            {
                right -= left;
            }
        }

        return left;
    }
}