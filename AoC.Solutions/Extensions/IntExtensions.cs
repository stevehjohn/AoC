namespace AoC.Solutions.Extensions;

public static class IntExtensions
{
    public static int DecrementRotate(this int input, int rotateAmount, int decrementBy = 1)
    {
        input -= decrementBy;

        if (input < 0)
        {
            input += rotateAmount;
        }

        return input;
    }

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
    
    public static int CountBits(this int value)
    {
        var count = 0;

        while (value > 0)
        {
            count++;

            value &= value - 1;
        }

        return count;
    }

    public static void Repetitions(this int times, Action action)
    {
        for (var i = 0; i < times; i++)
        {
            action();
        }
    }
}