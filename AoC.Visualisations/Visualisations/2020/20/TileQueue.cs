using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace AoC.Visualisations.Visualisations._2020._20;

public class TileQueue
{
    private readonly List<Tile> _imageSegments;

    private readonly Texture2D _image;

    private readonly Texture2D _cell;

    private const int QueueSize = Constants.TileSize * Constants.JigsawSize + Constants.TilePadding * (Constants.JigsawSize + 1);

    private const int Top = Constants.ScreenHeight / 2 - QueueSize / 2;

    private const int Left = Constants.ScreenWidth - Top - QueueSize;

    public TileQueue(List<Tile> imageSegments, Texture2D image, Texture2D cell)
    {
        _imageSegments = imageSegments;

        _image = image;

        _cell = cell;
    }

    public void Draw(SpriteBatch spriteBatch)
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

                var tile = _imageSegments[i];

                var spriteEffects = tile.Transform.Contains('H') ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

                spriteEffects |= tile.Transform.Contains('V') ? SpriteEffects.FlipVertically : SpriteEffects.None;

                var rotation = (float) Math.PI / 2f * tile.Transform.Count(c => c == 'R');

                spriteBatch.Draw(_image, new Vector2(screenX + Constants.TilePadding + offset, screenY + Constants.TilePadding + offset), tile.ImageSegment, Color.White, rotation, origin, Vector2.One, spriteEffects, 1);

                i++;

                if (i > _imageSegments.Count)
                {
                    break;
                }
            }

            if (i > _imageSegments.Count)
            {
                break;
            }
        }
    }
}