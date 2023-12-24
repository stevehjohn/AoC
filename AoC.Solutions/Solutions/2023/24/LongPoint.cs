namespace AoC.Solutions.Solutions._2023._24;

public class LongPoint
{
    public long X { get; set; }
    
    public long Y { get; set; }

    public static LongPoint Parse(string input)
    {
        var split = input.Split(',', StringSplitOptions.TrimEntries);

        var point = new LongPoint
        {
            X = int.Parse(split[0]),
            Y = int.Parse(split[1])
        };

        // if (split.Length > 2)
        // {
        //     point.Z = int.Parse(split[2]);
        // }

        return point;
    }
}