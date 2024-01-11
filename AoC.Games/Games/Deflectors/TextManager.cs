using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AoC.Games.Games.Deflectors;

public class TextManager : IActor
{
    private SpriteFont _font;
    
    public string Message { get; set; }
    
    public void LoadContent(ContentManager contentManager)
    {
        _font = contentManager.Load<SpriteFont>("font");
    }

    public void Update()
    {
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (Message == null)
        {
            return;
        }

        var w = _font.MeasureString(Message).X;

        // ReSharper disable once PossibleLossOfFraction
        var start = ArenaManager.TileSize * ArenaManager.MapSize / 2 - w / 2;

        for (var y = -2; y < 3; y++)
        {
            for (var x = -2; x < 3; x++)
            {
                spriteBatch.DrawString(_font, Message, new Vector2(start + x, 200 + y), Color.Black, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, .6f);
            }
        }

        spriteBatch.DrawString(_font, Message, new Vector2(start, 200), Color.FromNonPremultiplied(255, 255, 255, 255), 0, Vector2.Zero, Vector2.One, SpriteEffects.None, .7f);    
    }
}