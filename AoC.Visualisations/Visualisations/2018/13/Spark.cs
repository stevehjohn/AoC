namespace AoC.Visualisations.Visualisations._2018._13;

public class Spark
{
    public PointFloat Position { get; init; }

    public PointFloat Vector { get; init; }

    public int Ticks { get; set; }
    
    public int StartTicks { get; init; }

    public int SpriteOffset { get; init; }

    public float YGravity { get; init; } = 0.1f;
}