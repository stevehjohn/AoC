namespace AoC.Visualisations.Visualisations._2019._18;

public class Spark
{
    public PointFloat Position { get; init; }

    public PointFloat Vector { get; init; }

    public int Ticks { get; set; }
    
    public int StartTicks { get; init; }

    public float YGravity { get; } = 0.1f;
}