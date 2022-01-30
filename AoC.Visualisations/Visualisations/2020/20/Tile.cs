using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace AoC.Visualisations.Visualisations._2020._20;

public class Tile
{
    public int Id { get; }

    public Rectangle ImageSegment { get; }

    public string Transform { get; }

    public Tile(int id, Rectangle imageSegment, string transform)
    {
        Id = id;

        ImageSegment = imageSegment;

        Transform = transform;
    }
}