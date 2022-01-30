using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace AoC.Visualisations.Visualisations._2020._20;

public class Jigsaw
{
    private readonly List<Tile> _imageSegments;

    private readonly Texture2D _image;

    private readonly Texture2D _mat;

    private const int MatSize = Constants.TileSize * Constants.JigsawSize + Constants.TilePadding * 2;

    private const int Top = Constants.ScreenHeight / 2 - MatSize / 2;

    private const int Left = Top;

    public Jigsaw(List<Tile> imageSegments, Texture2D image, Texture2D mat)
    {
        _imageSegments = imageSegments;

        _image = image;

        _mat = mat;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_mat, new Vector2(Left, Top), new Rectangle(0, 0, MatSize, MatSize), Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
    }
}