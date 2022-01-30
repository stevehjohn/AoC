﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace AoC.Visualisations.Visualisations._2020._20;

public class TileQueue
{
    public Queue<Tile> MatchedTiles { get; } = new();

    private readonly List<Tile> _imageSegments;

    private readonly Texture2D _image;

    private readonly Texture2D _cell;

    private readonly Texture2D _scanner;

    private readonly Queue<Tile> _scanQueue = new();

    private Tile _scanningFor;

    private int _scannerIndex;

    private int _scannerDirection;

    private const int QueueSize = Constants.TileSize * Constants.JigsawSize + Constants.TilePadding * (Constants.JigsawSize + 1);

    private const int Top = Constants.ScreenHeight / 2 - QueueSize / 2;

    private const int Left = Constants.ScreenWidth - Top - QueueSize;

    public TileQueue(List<Tile> imageSegments, Texture2D image, Texture2D cell, Texture2D scanner)
    {
        _imageSegments = imageSegments;

        _image = image;

        _cell = cell;

        _scanner = scanner;
    }

    public void StartScan(int tileId)
    {
        if (tileId == -1)
        {
            _scanQueue.Enqueue(_imageSegments[0]);
        }
        else
        {
            _scanQueue.Enqueue(_imageSegments.Single(s => s.Id == tileId));
        }
    }

    public void Update()
    {
        if (_scanningFor != null)
        {
            if (_imageSegments[_scannerIndex] == _scanningFor)
            {
                MatchedTiles.Enqueue(_scanningFor);

                _imageSegments.Remove(_scanningFor);

                _scanningFor = null;
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
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        DrawQueue(spriteBatch);
    }

    private void DrawQueue(SpriteBatch spriteBatch)
    {
        var i = 0;

        var offset = Constants.TileSize / 2f;

        var origin = new Vector2(offset, offset);

        for (var y = 0; y < Constants.JigsawSize; y++)
        {
            for (var x = 0; x < Constants.JigsawSize; x++)
            {
                var screenX = Left + x * (Constants.TileSize + Constants.TilePadding);

                var screenY = Top + y * (Constants.TileSize + Constants.TilePadding);

                spriteBatch.Draw(_cell, new Vector2(screenX, screenY), new Rectangle(0, 0, Constants.TileSize + Constants.TilePadding * 2, Constants.TileSize + Constants.TilePadding * 2), Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);

                if (i >= _imageSegments.Count)
                {
                    continue;
                }

                var tile = _imageSegments[i];

                var spriteEffects = tile.Transform.Contains('H') ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

                spriteEffects |= tile.Transform.Contains('V') ? SpriteEffects.FlipVertically : SpriteEffects.None;

                var rotation = (float) Math.PI / 2f * tile.Transform.Count(c => c == 'R');

                spriteBatch.Draw(_image, new Vector2(screenX + Constants.TilePadding + offset, screenY + Constants.TilePadding + offset), tile.ImageSegment, Color.White, rotation, origin, Vector2.One, spriteEffects, 1);

                i++;
            }
        }
    }
}