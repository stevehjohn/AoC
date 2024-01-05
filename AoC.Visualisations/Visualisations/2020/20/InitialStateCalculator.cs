using AoC.Solutions.Common;
using AoC.Solutions.Solutions._2020._20;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace AoC.Visualisations.Visualisations._2020._20;

public class TileCoordinatesCalculator
{
    private readonly Part1 _puzzle;

    public List<Tile> ImageSegments { get; } = [];

    public TileCoordinatesCalculator(Part1 puzzle)
    {
        _puzzle = puzzle;
    }

    public void CalculateTileCoordinatesInImage()
    {
        _puzzle.GetAnswer();

        var jigsaw = _puzzle.Jigsaw;

        var oX = jigsaw.Min(t => t.Key.X);

        var oY = jigsaw.Min(t => t.Key.Y);

        var tilePositions = jigsaw.Select(t => (t.Value.Id, Position: new Point(t.Key.X - oX, t.Key.Y - oY))).ToList();

        var initialOrder = _puzzle.InitialTileOrder;

        var transforms = _puzzle.Transforms;

        foreach (var id in initialOrder)
        {
            var jigsawTile = tilePositions.Single(p => p.Id == id);

            if (! transforms.TryGetValue(jigsawTile.Id, out var transform))
            {
                transform = string.Empty;
            }

            ImageSegments.Add(new Tile(id, 
                                       new Rectangle(jigsawTile.Position.X * Constants.TileSize, jigsawTile.Position.Y * Constants.TileSize, Constants.TileSize, Constants.TileSize), 
                                       transform, 
                                       new Microsoft.Xna.Framework.Point(jigsawTile.Position.X, jigsawTile.Position.Y)));
        }
    }
}