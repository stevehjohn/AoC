using AoC.Solutions.Solutions._2023._16;
using AoC.Visualisations.Exceptions;
using AoC.Visualisations.Infrastructure;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;

namespace AoC.Visualisations.Visualisations._2023._16;

[UsedImplicitly]
public class Visualisation : VisualisationBase<PuzzleState>
{
    private SpriteBatch _spriteBatch;
    
    private Texture2D _sprites;

    private PuzzleState _state;

    private int _beam;

    private readonly Dictionary<int, List<(int X, int Y, char Direction)>> _beams = new();
    
    public Visualisation()
    {
        GraphicsDeviceManager = new GraphicsDeviceManager(this)
                                 {
                                     PreferredBackBufferWidth = 896,
                                     PreferredBackBufferHeight = 896
                                 };

        Content.RootDirectory = "./14";

        IgnoreQueueLimit = true;
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

    protected override void Initialize()
    {
        IsMouseVisible = true;

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _sprites = Content.Load<Texture2D>("sprites");

        base.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        if (HasNextState)
        {
            _state = GetNextState();

            if (_state.Beams != null)
            {
                foreach (var beam in _state.Beams)
                {
                    if (! _beams.ContainsKey(beam.Id))
                    { 
                        _beams.Add(beam.Id, new List<(int X, int Y, char Direction)>());
                    }
                    
                    _beams[beam.Id].Add((beam.X, beam.Y, beam.Direction));
                }
            }
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin(SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp);

        DrawMap();
        
        _spriteBatch.End();

        base.Draw(gameTime);
    }

    private void DrawMap()
    {
        var map = _state.Map;

        for (var y = 0; y < map.GetLength(1); y++)
        {
            for (var x = 0; x < map.GetLength(0); x++)
            {
                switch (map[x, y])
                {
                    case '\\':
                        break;
                    
                    case '/':
                        break;
                    
                    case '|':
                        break;
                    
                    case '-':
                        break;
                }
            }
        }
    }
}