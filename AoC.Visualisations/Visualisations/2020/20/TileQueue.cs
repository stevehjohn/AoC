using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;
using Point = Microsoft.Xna.Framework.Point;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace AoC.Visualisations.Visualisations._2020._20;

public class TileQueue
{
    public (Tile Tile, Point ScreenPosition)? MatchedTile
    {
        get
        {
            if (_matchedTile == null)
            {
                return null;
            }

            var matchedTile = _matchedTile;

            _imageSegments.Remove(_matchedTile.Value.Tile);

            _matchedTile = null;

            _scanningFor = null;

            _scannerFlashTicks = ScannerDefaultFlashTicks;

            return matchedTile;
        }
    }

    public bool IsEmpty => _scanQueue.Count == 0;

    private const float ScannerDefaultAlpha = 0.5f;

    private const int ScannerDefaultFlashTicks = 50;

    private readonly List<Tile> _imageSegments;

    private readonly Texture2D _image;

    private readonly Texture2D _cell;

    private readonly Texture2D _scanner;

    private readonly Queue<Tile> _scanQueue = new();

    private Tile _scanningFor;

    private int _scannerIndex;

    private int _scannerDirection;

    private float _scannerAlpha = ScannerDefaultAlpha;

    private int _scannerFlashTicks = ScannerDefaultFlashTicks;

    private const int QueueSize = Constants.TileSize * Constants.JigsawSize + Constants.TilePadding * (Constants.JigsawSize + 1);

    private const int Top = Constants.ScreenHeight / 2 - QueueSize / 2;

    private const int Left = Constants.ScreenWidth - Top - QueueSize;

    private (Tile Tile, Point ScreenPosition)? _matchedTile;

    public TileQueue(List<Tile> imageSegments, Texture2D image, Texture2D cell, Texture2D scanner)
    {
        _imageSegments = imageSegments;

        _image = image;

        _cell = cell;

        _scanner = scanner;
    }

    public void StartScan(int tileId)
    {
        _scanQueue.Enqueue(tileId == -1
                               ? _imageSegments[0]
                               : _imageSegments.Single(s => s.Id == tileId));
    }

    public void Update()
    {
        if (_scanningFor != null)
        {
            if (_imageSegments[_scannerIndex] == _scanningFor)
            {
                _scannerAlpha -= 0.01f;

                if (_scannerAlpha < 0)
                {
                    _scannerAlpha = ScannerDefaultAlpha;
                }

                _scannerFlashTicks--;

                if (_matchedTile == null && _scannerFlashTicks < 0)
                {
                    var (screenX, screenY) = GetScreenCoordinates(_scannerIndex % Constants.JigsawSize, _scannerIndex / Constants.JigsawSize);

                    _matchedTile = (_scanningFor, new Point(screenX, screenY));
                }
            }
            else
            {
                _scannerIndex += _scannerDirection;
            }

            return;
        }

        if (_scanQueue.Count > 0)
        {
            _scanningFor = _scanQueue.Dequeue();

            var targetIndex = _imageSegments.IndexOf(_scanningFor);

            if (targetIndex > _scannerIndex)
            {
                _scannerDirection = 1;
            }
            else if (targetIndex < _scannerIndex)
            {
                _scannerDirection = -1;
            }

            if (_scannerIndex == 0)
            {
                _scannerIndex = 0;
            }
            else if (_scannerIndex >= _imageSegments.Count)
            {
                _scannerIndex = _imageSegments.Count - 1;
            }

            _scannerAlpha = ScannerDefaultAlpha;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        DrawQueue(spriteBatch);

        if (_scanQueue.Count > 0)
        {
            DrawScanner(spriteBatch);
        }
    }

    private void DrawScanner(SpriteBatch spriteBatch)
    {
        var x = _scannerIndex % Constants.JigsawSize;

        var y = _scannerIndex / Constants.JigsawSize;

        var (screenX, screenY) = GetScreenCoordinates(x, y);

        spriteBatch.Draw(_scanner, new Vector2(screenX + Constants.TilePadding, screenY + Constants.TilePadding), new Rectangle(0, 0, Constants.TileSize, Constants.TileSize), Color.White * _scannerAlpha, 0, Vector2.Zero, Vector2.One,
                         SpriteEffects.None, 0.1f);
    }

    private void DrawQueue(SpriteBatch spriteBatch)
    {
        var i = 0;

        const float offset = Constants.TileSize / 2f;

        var origin = new Vector2(offset, offset);

        for (var y = 0; y < Constants.JigsawSize; y++)
        {
            for (var x = 0; x < Constants.JigsawSize; x++)
            {
                var (screenX, screenY) = GetScreenCoordinates(x, y);

                spriteBatch.Draw(_cell, new Vector2(screenX, screenY), new Rectangle(0, 0, Constants.TileSize + Constants.TilePadding * 2, Constants.TileSize + Constants.TilePadding * 2), Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None,
                                 0f);

                if (i >= _imageSegments.Count)
                {
                    continue;
                }

                var tile = _imageSegments[i];

                var spriteEffects = tile.Transform.Contains('H') ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

                spriteEffects |= tile.Transform.Contains('V') ? SpriteEffects.FlipVertically : SpriteEffects.None;

                var rotation = (float) Math.PI / 2f * tile.Transform.Count(c => c == 'R');

                spriteBatch.Draw(_image, new Vector2(screenX + Constants.TilePadding + offset, screenY + Constants.TilePadding + offset), tile.ImageSegment, Color.White, rotation, origin, Vector2.One, spriteEffects, 0.05f);

                i++;
            }
        }
    }

    private static (int X, int Y) GetScreenCoordinates(int x, int y)
    {
        return (Left + x * (Constants.TileSize + Constants.TilePadding), Top + y * (Constants.TileSize + Constants.TilePadding));
    }
}