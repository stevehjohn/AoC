using AoC.Visualisations.Exceptions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;
using Point = Microsoft.Xna.Framework.Point;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace AoC.Visualisations.Visualisations._2020._20;

public class Jigsaw
{
    public bool CanTakeTile => _currentTile == null && _puzzleOrigin.Equals(_targetOrigin);

    private readonly List<Tile> _jigsaw = new();

    private readonly Texture2D _image;

    private readonly Texture2D _mat;

    private readonly Texture2D _border;

    private const int MatSize = Constants.TileSize * Constants.JigsawSize + Constants.TilePadding * 2;

    private const int Top = Constants.ScreenHeight / 2 - MatSize / 2;

    private const int Left = Top;

    private const int CentreY = Top + Constants.TilePadding + Constants.TileSize * (Constants.JigsawSize - 1) / 2;

    private const int CentreX = Left + Constants.TilePadding + Constants.TileSize * (Constants.JigsawSize - 1) / 2;

    private Tile _currentTile;

    private Point _puzzleSize;

    private Point _puzzleOrigin = new(CentreX, CentreY);

    private Point? _targetOrigin = new(CentreX, CentreY);

    public Jigsaw(Texture2D image, Texture2D mat, Texture2D border)
    {
        _image = image;

        _mat = mat;

        _border = border;
    }

    public void AddTile(Tile tile)
    {
        if (_currentTile != null)
        {
            throw new VisualisationException("Cannot take another tile at this time.");
        }

        _currentTile = tile;

        var xMin = _jigsaw.Count == 0 ? -1 : _jigsaw.Min(t => t.PositionInPuzzle.X);

        var yMin = _jigsaw.Count == 0 ? -1 : _jigsaw.Min(t => t.PositionInPuzzle.Y);

        _jigsaw.Add(tile);

        var width = (_jigsaw.Max(t => t.PositionInPuzzle.X) - _jigsaw.Min(t => t.PositionInPuzzle.X)) * Constants.TileSize;

        var height = (_jigsaw.Max(t => t.PositionInPuzzle.Y) - _jigsaw.Min(t => t.PositionInPuzzle.Y)) * Constants.TileSize;

        _puzzleSize = new Point(width, height);

        _targetOrigin = new Point(CentreX - _puzzleSize.X / 2, CentreY - _puzzleSize.Y / 2);

        if (_jigsaw.Min(t => t.PositionInPuzzle.X) < xMin)
        {
            _puzzleOrigin.X -= Constants.TileSize;
        }

        if (_jigsaw.Min(t => t.PositionInPuzzle.Y) < yMin)
        {
            _puzzleOrigin.Y -= Constants.TileSize;
        }
    }

    public void Update()
    {
        if (_targetOrigin != null)
        {
            if (Math.Abs(_puzzleOrigin.X - _targetOrigin.Value.X) < 2 && Math.Abs(_puzzleOrigin.Y - _targetOrigin.Value.Y) < 2)
            {
                _puzzleOrigin = _targetOrigin.Value;

                _currentTile = null;

                return;
            }

            if (! _puzzleOrigin.Equals(_targetOrigin))
            {
                _puzzleOrigin.X += Math.Sign(_targetOrigin.Value.X - _puzzleOrigin.X) * 2;

                _puzzleOrigin.Y += Math.Sign(_targetOrigin.Value.Y - _puzzleOrigin.Y) * 2;

                return;
            }
        }

        _currentTile = null;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_mat, new Vector2(Left, Top), new Rectangle(0, 0, MatSize, MatSize), Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);

        DrawPieces(spriteBatch);

        spriteBatch.Draw(_border, new Vector2(Left - Constants.TileSize, Top - Constants.TileSize), new Rectangle(0, 0, MatSize + Constants.TileSize * 2, MatSize + Constants.TileSize * 2), Color.White, 0, Vector2.Zero, Vector2.One,
                         SpriteEffects.None, 0.1f);
    }

    public Vector2 GetTilePosition(Tile tile)
    {
        int xMin;

        int yMin;

        if (_jigsaw.Count > 0)
        {
            xMin = _jigsaw.Min(t => t.PositionInPuzzle.X);

            yMin = _jigsaw.Min(t => t.PositionInPuzzle.Y);
        }
        else
        {
            xMin = tile.PositionInPuzzle.X;

            yMin = tile.PositionInPuzzle.Y;
        }

        return new Vector2(_puzzleOrigin.X + (tile.PositionInPuzzle.X - xMin) * Constants.TileSize, _puzzleOrigin.Y + (tile.PositionInPuzzle.Y - yMin) * Constants.TileSize);
    }

    private void DrawPieces(SpriteBatch spriteBatch)
    {
        if (_jigsaw.Count == 0)
        {
            return;
        }

        foreach (var tile in _jigsaw)
        {
            spriteBatch.Draw(_image,
                             GetTilePosition(tile),
                             new Rectangle(tile.PositionInPuzzle.X * Constants.TileSize, tile.PositionInPuzzle.Y * Constants.TileSize, Constants.TileSize, Constants.TileSize),
                             Color.White,
                             0,
                             Vector2.Zero,
                             Vector2.One,
                             SpriteEffects.None, 0.05f);
        }
    }
}