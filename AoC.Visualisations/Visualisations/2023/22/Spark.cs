using Microsoft.Xna.Framework;

namespace AoC.Visualisations.Visualisations._2023._22;

public class Spark
{
    public PointFloat Position { get; set; }

    public PointFloat Vector { get; set; }

    public int Ticks { get; set; }
    
    public int StartTicks { get; set; }

    public int SpriteOffset { get; set; }
    
    public Color Color { get; set; }

    public float YGravity { get; set; } = 0.1f;
}