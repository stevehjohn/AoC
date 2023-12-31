namespace AoC.Solutions.Extensions;

public static class StringExtensions
{
    public static int CommonCharacterCount(this string left, string right)
    {
        if (right.Length > left.Length)
        {
            (right, left) = (left, right);
        }

        var count = 0;

        for (var i = 0; i < left.Length; i++)
        {
            if (right.Contains(left[i]))
            {
                count++;
            }
        }

        return count;
    }

    public static char[,] To2DArray(this string input)
    {
        var lines = input.Split('\n');

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