using Point = Microsoft.Xna.Framework.Point;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace AoC.Visualisations.Visualisations._2020._20;

public class Tile
{
    public Point ScreenPosition { get; set; }

    public Rectangle ImageSegment { get; set; }

    public string Transform { get; set; }
}