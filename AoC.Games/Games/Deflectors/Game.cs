using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AoC.Games.Games.Deflectors;

public class Game : Microsoft.Xna.Framework.Game
{
    private const int MapSize = 30;

    private const int TileSize = 21;
    
    private GraphicsDeviceManager _graphics;
    
    private SpriteBatch _spriteBatch;

    private Texture2D _beams;

    private Texture2D _mirrors;

    private Texture2D _other;
    
    public Game()
    {
        _graphics = new GraphicsDeviceManager(this)
        {
            PreferredBackBufferWidth = 693,
            PreferredBackBufferHeight = 631
        };
        
        Content.RootDirectory = "./Deflectors";
        
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _beams = Content.Load<Texture2D>("beams");

        _mirrors = Content.Load<Texture2D>("mirrors");

        _other = Content.Load<Texture2D>("other");
    }

    protected override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin(SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp);

        DrawBackground();

        _spriteBatch.End();
        
        base.Draw(gameTime);
    }

    private void DrawBackground()
    {
        for (var y = 0; y < MapSize; y++)
        {
            for (var x = 0; x < MapSize; x++)
            {
                _spriteBatch.Draw(_other,
                    new Vector2(x * TileSize, y * TileSize),
                    new Rectangle(TileSize * 3 + 1, 0, TileSize, TileSize),
                    Color.FromNonPremultiplied(255, 255, 255, 50), 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);            
            }

            if (y > 0 && y < MapSize - 1)
            {
                _spriteBatch.Draw(_other,
                    new Vector2((MapSize + 1) * TileSize, y * TileSize),
                    new Rectangle(TileSize * 3 + 1, 0, TileSize, TileSize),
                    Color.FromNonPremultiplied(255, 255, 255, 50), 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);            
            }
        }
    }
}