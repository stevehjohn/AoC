namespace AoC.Solutions.Solutions._2023._24;

public class DoublePoint
{
    public double X { get; set; }
    
    public double Y { get; set; }

    public static DoublePoint Parse(string input)
    {
        var split = input.Split(',', StringSplitOptions.TrimEntries);

        var point = new DoublePoint
        {
            X = double.Parse(split[0]),
            Y = double.Parse(split[1])
        };

        // if (split.Length > 2)
        // {
        //     point.Z = int.Parse(split[2]);
        // }

        return point;
    }
}