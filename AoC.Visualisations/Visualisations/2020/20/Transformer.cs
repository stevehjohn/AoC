using AoC.Visualisations.Exceptions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;
using Point = Microsoft.Xna.Framework.Point;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace AoC.Visualisations.Visualisations._2020._20;

public class Transformer
{
    private const int MoveFrames = 50;

    public bool CanTakeTile => _currentTile == null;

    public Tile TransformedTile
    {
        get
        {
            var tile = _transformedTile;

            _transformedTile = null;

            return tile;
        }
    }

    private readonly Texture2D _image;

    private readonly Texture2D _cell;

    private Tile _currentTile;

    private Vector2 _position;

    private Vector2 _vector;

    private Vector2 _jigsawPosition;

    private int _frame;

    private float _scale;

    private float _scaleDelta;

    private int _phase;

    private Tile _transformedTile;

    public Transformer(Texture2D image, Texture2D cell)
    {
        _image = image;

        _cell = cell;
    }

    public void AddTile(Tile tile, Point position, Vector2 jigsawPosition)
    {
        if (_currentTile != null)
        {
            throw new VisualisationException("Cannot take another tile at this time.");
        }

        _currentTile = tile;

        _position = new Vector2(position.X, position.Y);

        _vector = new Vector2((Constants.ScreenWidth / 2f - Constants.TilePadding * 2 - position.X - 6) / MoveFrames, (Constants.ScreenHeight / 2f - Constants.TilePadding * 2 - position.Y) / MoveFrames);

        _frame = MoveFrames;

        _scale = 1;

        _scaleDelta = 1f / MoveFrames;

        _jigsawPosition = jigsawPosition;

        _phase = 0;
    }

    public void Update()
    {
        if (_currentTile == null)
        {
            return;
        }

        switch (_phase)
        {
            case 0:
                if (_frame > 0)
                {
                    MoveTile();

                    return;
                }

                _vector = new Vector2((_jigsawPosition.X - _position.X) / MoveFrames, (_jigsawPosition.Y - _position.Y) / MoveFrames);

                _frame = MoveFrames;

                _scaleDelta = -_scaleDelta;

                _phase = 2;

                break;

            case 2:
                if (_frame > 0)
                {
                    MoveTile();

                    return;
                }

                _phase = 3;

                break;

            case 3:
                _currentTile.Transform = string.Empty;

                _position = _jigsawPosition;

                _transformedTile = _currentTile;

                _currentTile = null;

                break;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (_currentTile == null)
        {
            return;
        }

        var offset = Constants.TileSize / 2f;

        var origin = new Vector2(offset, offset);

        spriteBatch.Draw(_cell, 
                         new Vector2(_position.X, _position.Y), 
                         new Rectangle(0, 0, Constants.TileSize + Constants.TilePadding * 2, Constants.TileSize + Constants.TilePadding * 2), 
                         Color.White, 
                         0, 
                         origin, 
                         _scale, 
                         SpriteEffects.None,
                         0.5f);

        var spriteEffects = _currentTile.Transform.Contains('H') ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

        spriteEffects |= _currentTile.Transform.Contains('V') ? SpriteEffects.FlipVertically : SpriteEffects.None;

        var rotation = (float) Math.PI / 2f * _currentTile.Transform.Count(c => c == 'R');

        spriteBatch.Draw(_image,
                         new Vector2(_position.X + Constants.TilePadding * _scale, _position.Y + Constants.TilePadding * _scale),
                         new Rectangle(_currentTile.PositionInPuzzle.X * Constants.TileSize, _currentTile.PositionInPuzzle.Y * Constants.TileSize, Constants.TileSize, Constants.TileSize),
                         Color.White,
                         rotation,
                         origin,
                         _scale,
                         spriteEffects,
                         0.55f);
    }

    private void MoveTile()
    {
        _position.X += _vector.X;

        _position.Y += _vector.Y;

        _scale += _scaleDelta;

        _frame--;
    }
}