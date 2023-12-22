using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2023._22;

public class Brick
{
    public List<Point> Points { get; set; } = new();

    public List<Brick> Supports { get; set; } = new();
}