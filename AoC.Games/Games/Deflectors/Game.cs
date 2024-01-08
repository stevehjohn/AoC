using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AoC.Games.Games.Deflectors;

public class Game : Microsoft.Xna.Framework.Game
{
    private const int MapSize = 30;
    
    private GraphicsDeviceManager _graphics;
    
    private SpriteBatch _spriteBatch;

    private Texture2D _beams;

    private Texture2D _mirrors;

    private Texture2D _other;
    
    public Game()
    {
        _graphics = new GraphicsDeviceManager(this)
        {
            PreferredBackBufferWidth = 672,
            PreferredBackBufferHeight = 630
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

        base.Draw(gameTime);
    }
}