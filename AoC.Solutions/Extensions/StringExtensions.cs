namespace AoC.Solutions.Extensions;

public static class StringExtensions
{
    extension(string left)
    {
        public int CommonCharacterCount(string right)
        {
            Span<int> leftCounts = stackalloc int[128];

            Span<int> rightCounts = stackalloc int[128];

            foreach (var c in left)
            {
                leftCounts[c]++;
            }

            foreach (var c in right)
            {
                rightCounts[c]++;
            }

            var count = 0;

            for (var i = 0; i < 128; i++)
            {
                count += Math.Min(leftCounts[i], rightCounts[i]);
            }

            return count;
        }

        public char[,] To2DArray()
        {
            var lines = left.Split('\n');

            var result = new char[lines[0].Length, lines.Length];

            var y = 0;

            foreach (var line in lines)
            {
                for (var x = 0; x < line.Length; x++)
                {
                    result[x, y] = line[x];
                }

                y++;
            }

            return result;
        }
    }

    extension(string input)
    {
        public int ParseToInt()
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

            for (; i < input.Length; i++)
            {
                var digit = input[i];

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