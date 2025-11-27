namespace AoC.Solutions.Extensions;

public static class IntExtensions
{
    extension(int input)
    {
        public int Converge(int target)
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

        public int CountBits()
        {
            var count = 0;

            while (input > 0)
            {
                count++;

                input &= input - 1;
            }

            return count;
        }

        public void Repetitions(Action action)
        {
            for (var i = 0; i < input; i++)
            {
                action();
            }
        }
    }
}