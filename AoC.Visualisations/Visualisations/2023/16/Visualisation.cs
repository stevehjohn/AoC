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
    
    private Texture2D _tiles;

    private PuzzleState _state;

    private readonly Dictionary<int, List<(int X, int Y, char Direction)>> _allBeams = new();

    private readonly List<List<(int X, int Y, char Direction)>> _beams = new ();
    
    private Color[] _palette;
    
    public Visualisation()
    {
        GraphicsDeviceManager = new GraphicsDeviceManager(this)
                                 {
                                     PreferredBackBufferWidth = 812,
                                     PreferredBackBufferHeight = 812
                                 };

        Content.RootDirectory = "./16";

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

        _palette = PaletteGenerator.GetPalette(26,
            new[]
            {
                new Color(254, 253, 0),
                new Color(254, 211, 56),
                new Color(255, 168, 76),
                new Color(176, 83, 203),
                new Color(119, 35, 172),
                new Color(46, 27, 134)
            });

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _tiles = Content.Load<Texture2D>("map-tiles");

        base.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        if (HasNextState)
        {
            _state = GetNextState();
        }

        if (_state.Beams != null && _allBeams.Count == 0)
        {
            foreach (var beam in _state.Beams)
            {
                if (! _allBeams.ContainsKey(beam.Id))
                { 
                    _allBeams.Add(beam.Id, new List<(int X, int Y, char Direction)>());
                }

                _allBeams[beam.Id].Add((beam.X, beam.Y, beam.Direction));
            }
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin(SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp);

        DrawMap();

        DrawBeams();
        
        _spriteBatch.End();

        base.Draw(gameTime);
    }

    private void DrawBeams()
    {
        if (_allBeams.Count == 0)
        {
            return;
        }
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
                        _spriteBatch.Draw(_tiles, new Vector2(22 + x * 7, 22 + y * 7), new Rectangle(14, 0, 7, 7), Color.Cyan, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, .1f);
                        break;
                    
                    case '/':
                        _spriteBatch.Draw(_tiles, new Vector2(22 + x * 7, 22 + y * 7), new Rectangle(21, 0, 7, 7), Color.Cyan, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, .1f);
                        break;
                    
                    case '|':
                        _spriteBatch.Draw(_tiles, new Vector2(22 + x * 7, 22 + y * 7), new Rectangle(7, 0, 7, 7), Color.Cyan, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, .1f);
                        break;
                    
                    case '-':
                        _spriteBatch.Draw(_tiles, new Vector2(22 + x * 7, 22 + y * 7), new Rectangle(0, 0, 7, 7), Color.Cyan, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, .1f);
                        break;
                }
            }
        }
    }
}