using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AoC.Games.Games.Deflectors.Actors;

public class TextManager : IActor
{
    private SpriteFont _font;
    
    public string Message { get; set; }

    private int _levelNumber;

    private int _beam;

    private int _score;

    private int _displayScore;

    private int _highScore;

    private int _displayHighScore;
    
    public void SetInformation(int levelNumber, int beam, int score, int highScore)
    {
        _levelNumber = levelNumber;

        _beam = beam;

        _score = score;

        _highScore = highScore;
    }

    public void LoadContent(ContentManager contentManager)
    {
        _font = contentManager.Load<SpriteFont>("font");
    }

    public void Update()
    {
        if (_displayScore < _score)
        {
            _displayScore++;
        }

        if (_displayScore > _score)
        {
            _displayScore = _score;
        }

        if (_displayHighScore < _highScore)
        {
            _displayHighScore++;
        }

        if (_displayHighScore > _highScore)
        {
            _displayHighScore = _highScore;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        DrawMessage(spriteBatch);
        
        DrawInfo(spriteBatch);
    }

    private void DrawMessage(SpriteBatch spriteBatch)
    {
        if (Message == null)
        {
            return;
        }

        var w = _font.MeasureString(Message).X;

        // ReSharper disable once PossibleLossOfFraction
        var start = Constants.TileSize * Constants.MapSize / 2 - w / 2;

        for (var y = -2; y < 3; y++)
        {
            for (var x = -2; x < 3; x++)
            {
                spriteBatch.DrawString(_font, Message, new Vector2(start + x, 200 + y), Color.Black, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, .6f);
            }
        }

        spriteBatch.DrawString(_font, Message, new Vector2(start, 200), Color.FromNonPremultiplied(255, 255, 255, 255), 0, Vector2.Zero, Vector2.One, SpriteEffects.None, .7f);    
    }

    private void DrawInfo(SpriteBatch spriteBatch)
    {
        spriteBatch.DrawString(_font, "LEVEL:", new Vector2(10, -2), Color.FromNonPremultiplied(0, 128, 0, 255));

        var x = _font.MeasureString("LEVEL: ").X + 10;

        spriteBatch.DrawString(_font, $"{_levelNumber,2}", new Vector2(x, -2), Color.FromNonPremultiplied(192, 192, 192, 255));

        spriteBatch.DrawString(_font, "BEAM:", new Vector2(170, -2), Color.FromNonPremultiplied(0, 128, 0, 255));

        x = _font.MeasureString("BEAM: ").X + 170;

        spriteBatch.DrawString(_font, $"{_beam / 3,3}", new Vector2(x, -2), Color.FromNonPremultiplied(192, 192, 192, 255));

        spriteBatch.DrawString(_font, "SCORE:", new Vector2(340, -2), Color.FromNonPremultiplied(0, 128, 0, 255));

        x = _font.MeasureString("SCORE: ").X + 340;

        spriteBatch.DrawString(_font, _displayScore.ToString(), new Vector2(x, -2), Color.FromNonPremultiplied(192, 192, 192, 255));

        spriteBatch.DrawString(_font, "HI:", new Vector2(530, -2), Color.FromNonPremultiplied(0, 128, 0, 255));

        x = _font.MeasureString("HI: ").X + 530;

        spriteBatch.DrawString(_font, _displayHighScore.ToString(), new Vector2(x, -2), Color.FromNonPremultiplied(192, 192, 192, 255));
    }
}