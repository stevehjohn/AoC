﻿using AoC.Visualisations.Exceptions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace AoC.Visualisations.Visualisations._2020._20;

public class Jigsaw
{
    public bool CanTakeTile => _currentTile == null;

    private readonly List<Tile> _imageSegments;

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

    public Jigsaw(List<Tile> imageSegments, Texture2D image, Texture2D mat, Texture2D border)
    {
        _imageSegments = imageSegments;

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

        _jigsaw.Add(tile);
    }

    public void Update()
    {
        _currentTile = null;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_mat, new Vector2(Left, Top), new Rectangle(0, 0, MatSize, MatSize), Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);

        DrawPieces(spriteBatch);

        spriteBatch.Draw(_border, new Vector2(Left - Constants.TileSize, Top - Constants.TileSize), new Rectangle(0, 0, MatSize + Constants.TileSize * 2, MatSize + Constants.TileSize * 2), Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0.1f);
    }


    private void DrawPieces(SpriteBatch spriteBatch)
    {
        foreach (var tile in _jigsaw)
        {
            spriteBatch.Draw(_image, 
                             new Vector2(tile.PositionInPuzzle.X * Constants.TileSize, tile.PositionInPuzzle.Y * Constants.TileSize), 
                             new Rectangle(tile.PositionInPuzzle.X * Constants.TileSize, tile.PositionInPuzzle.Y * Constants.TileSize, Constants.TileSize, Constants.TileSize), 
                             Color.White, 
                             0, 
                             Vector2.Zero, 
                             Vector2.One, 
                             SpriteEffects.None, 0.05f);
        }
    }
}