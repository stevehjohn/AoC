namespace AoC.Extensions;

public static class IntExtensions
{
    public static int Converge(this int input, int target)
    {
        if (input == target)
        {
            return input;
        }

        if (target > input)
        {
            return input + 1;
        }

        return input - 1;
    }
}