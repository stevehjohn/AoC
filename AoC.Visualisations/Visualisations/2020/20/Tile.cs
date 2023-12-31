using Point = Microsoft.Xna.Framework.Point;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace AoC.Visualisations.Visualisations._2020._20;

public class Tile
{
    public int Id { get; }

    public Rectangle ImageSegment { get; }

    public Point PositionInPuzzle { get; }

    public string Transform { get; set;  }

    public Tile(int id, Rectangle imageSegment, string transform, Point positionInPuzzle)
    {
        Id = id;

        ImageSegment = imageSegment;

        Transform = transform;

        PositionInPuzzle = positionInPuzzle;
    }
}