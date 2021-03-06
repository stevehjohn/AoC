namespace AoC.Visualisations.Visualisations._2018._13;

public class Spark
{
    public PointFloat Position { get; set; }

    public PointFloat Vector { get; set; }

    public int Ticks { get; set; }
    
    public int StartTicks { get; set; }

    public int SpriteOffset { get; set; }

    public float YGravity { get; set; } = 0.1f;
}