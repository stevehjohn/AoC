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
}