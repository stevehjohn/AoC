using Microsoft.Xna.Framework;

namespace AoC.Visualisations.Visualisations._2023._22;

public class Spark
{
    public PointFloat Position { get; init; }

    public PointFloat Vector { get; init; }

    public int Ticks { get; set; }
    
    public int StartTicks { get; init; }

    public int SpriteOffset { get; init; }
    
    public Color Color { get; init; }
    
    public float Z { get; init; }

    public float YGravity { get; } = 0.1f;
}