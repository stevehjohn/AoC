namespace AoC.Visualisations.Visualisations._2023._16;

public class Spark
{
    public PointFloat Position { get; init; }

    public PointFloat Vector { get; init; }

    public int Ticks { get; set; }
    
    public int StartTicks { get; init; }

    public int SpriteOffset { get; init; }

    public static float YGravity => 0.1f;
}