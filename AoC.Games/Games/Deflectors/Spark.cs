using Microsoft.Xna.Framework;

namespace AoC.Games.Games.Deflectors;

public class Spark
{
    public PointFloat Position { get; init; }

    public PointFloat Vector { get; init; }

    public int Ticks { get; set; }
    
    public int StartTicks { get; init; }

    public int SpriteOffset { get; init; }
    
    public Color Color { get; init; }
    
    public float YGravity => 0.1f;
}