using AoC.Solutions.Solutions._2023._22;
using AoC.Visualisations.Exceptions;
using AoC.Visualisations.Infrastructure;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AoC.Visualisations.Visualisations._2023._22;

[UsedImplicitly]
public class Visualisation : VisualisationBase<PuzzleState>
{
    private const int TileWidth = 42;

    private const int TileHeight = 38;
    
    private const int HalfTileWidth = 21;

    private const int HalfTileHeight = 19;
    
    private const int TileIsoHeight = 10;
    
    private SpriteBatch _spriteBatch;
    
    private Texture2D _tile;

    private PuzzleState _state;

    public Visualisation()
    {
        GraphicsDeviceManager = new GraphicsDeviceManager(this)
        {
            PreferredBackBufferWidth = 430,
            PreferredBackBufferHeight = 980
        };

        Content.RootDirectory = "./22";
    }

    public override void SetPart(int part)
    {
        switch (part)
        {
            case 1:
                Puzzle = new Part1(this);

                break;

            case 2:
                Puzzle = new Part2(this);

                break;

            default:
                throw new VisualisationParameterException();
        }
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _tile = Content.Load<Texture2D>("tile");
    }

    protected override void Update(GameTime gameTime)
    {
        if (HasNextState)
        {
            _state = GetNextState();
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin(SpriteSortMode.Immediate, samplerState: SamplerState.PointClamp);
        
        DrawBricks();
        
        _spriteBatch.End();
        
        base.Draw(gameTime);
    }

    private void DrawBricks()
    {
        for (var z = 1; z < _state.Height; z++)
        {
            for (var x = 9; x >= 0; x--)
            {
                for (var y = 0; y < 10; y++)
                {
                    var id = _state.Map[z, x, y];

                    /*
                     * TODO: Sort draw order. Scroll up as they settle.
                     */
                    
                    if (id > 0)
                    {
                        _spriteBatch.Draw(_tile, 
                            new Vector2(195 + (x - y) * HalfTileWidth, 970 - (TileIsoHeight * z + (x + y) * (TileIsoHeight + 4))), 
                            new Rectangle(0, 0, TileWidth, TileHeight),
                            GetBrickColor(id), 0, Vector2.Zero, Vector2.One, SpriteEffects.None, .9f);
                    }
                }
            }
        }
    }

    private static Color GetBrickColor(int id)
    {
        return (id % 14) switch
        {
            1 => Color.FromNonPremultiplied(0, 0, 255, 255),
            2 => Color.FromNonPremultiplied(255, 0, 0, 255),
            3 => Color.FromNonPremultiplied(255, 0, 255, 255),
            4 => Color.FromNonPremultiplied(0, 255, 0, 255),
            5 => Color.FromNonPremultiplied(0, 255, 255, 255),
            6 => Color.FromNonPremultiplied(255, 255, 0, 255),
            7 => Color.FromNonPremultiplied(255, 255, 255, 255),
            8 => Color.FromNonPremultiplied(0, 0, 192, 255),
            9 => Color.FromNonPremultiplied(192, 0, 0, 255),
            10 => Color.FromNonPremultiplied(192, 0, 192, 255),
            11 => Color.FromNonPremultiplied(0, 192, 0, 255),
            12 => Color.FromNonPremultiplied(0, 192, 192, 255),
            13 => Color.FromNonPremultiplied(192, 192, 0, 255),
            _ => Color.FromNonPremultiplied(192, 192, 192, 255)
        };
    }
}