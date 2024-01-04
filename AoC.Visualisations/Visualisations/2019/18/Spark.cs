namespace AoC.Visualisations.Visualisations._2019._18;

public class Spark
{
    public PointFloat Position { get; set; }

    public PointFloat Vector { get; set; }

    public int Ticks { get; set; }
    
    public int StartTicks { get; set; }

    public float YGravity { get; } = 0.1f;
}