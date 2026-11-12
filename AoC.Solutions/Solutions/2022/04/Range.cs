using AoC.Solutions.Extensions;

namespace AoC.Solutions.Solutions._2022._04;

public class Range
{
    public int Left { get; }

    public int Right { get; }

    public Range(string data)
    {
        var split = data.Split('-');

        Left = int.FastParse(split[0]);

        Right = int.FastParse(split[1]);
    }
}