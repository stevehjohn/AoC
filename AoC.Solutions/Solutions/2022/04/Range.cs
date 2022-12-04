namespace AoC.Solutions.Solutions._2022._04;

public class Range
{
    public int Left { get; }

    public int Right { get; }

    public Range(string data)
    {
        var split = data.Split('-');

        Left = int.Parse(split[0]);

        Right = int.Parse(split[1]);
    }
}