using System.Numerics;

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
            return BitOperations.PopCount((uint) input);
        }

        public void Repetitions(Action action)
        {
            for (var i = 0; i < input; i++)
            {
                action();
            }
        }
    }

    extension(int)
    {
        public static int FastParse(string input)
        {
            return FastParse(input.AsSpan());
        }
        
        public static int FastParse(ReadOnlySpan<char> input)
        {
            if (input.Length == 0)
            {
                throw new FormatException("Input string is empty.");
            }

            var result = 0;

            var i = 0;

            var negative = false;

            if (input[0] == '-')
            {
                negative = true;

                i = 1;
            }

            if (input[0] == '+')
            {
                i = 1;
            }

            for (; i < input.Length; i++)
            {
                var digit = input[i];

                if (char.IsWhiteSpace(digit) || digit == ',')
                {
                    continue;
                }

                if (digit is < '0' or > '9')
                {
                    break;
                }

                result = negative
                    ? result * 10 - (digit - '0')
                    : result * 10 + (digit - '0');
            }

            if (i == (negative ? 1 : 0))
            {
                throw new FormatException("No integer input.");
            }

            return result;
        }
    }
}