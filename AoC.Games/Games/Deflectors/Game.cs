using AoC.Games.Games.Deflectors.Levels;
using AoC.Games.Infrastructure;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AoC.Games.Games.Deflectors;

public class Game : Microsoft.Xna.Framework.Game
{
    private const int MapSize = 30;

    private const int TileSize = 21;

    private const int BeamSize = 7;

    private const int BeamFactor = TileSize / BeamSize;
    
    // ReSharper disable once NotAccessedField.Local
    private GraphicsDeviceManager _graphics;
    
    private SpriteBatch _spriteBatch;

    private Texture2D _beams;

    private Texture2D _mirrors;

    private Texture2D _other;

    private readonly LevelDataProvider _levels = new();

    private Level _level;
    
    private Color[] _palette;

    private int _paletteStart;

    private char _mirror;
    
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
        _levels.LoadLevels();

        _level = _levels.GetLevel(1);
        
        _palette = PaletteGenerator.GetPalette(26,
        [
            new Color(46, 27, 134),
            new Color(119, 35, 172),
            new Color(176, 83, 203),
            new Color(255, 168, 76),
            new Color(254, 211, 56),
            new Color(254, 253, 0)
        ]);

        _mirror = _level.Pieces[0];
        
        _level.Pieces.RemoveAt(0);
        
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
        
        DrawPieces();

        DrawStarts();

        DrawEnds();

        DrawMirrors();

        DrawBeams();
        
        _spriteBatch.End();
        
        base.Draw(gameTime);
    }

    private void DrawBeams()
    {
        foreach (var start in _level.Starts)
        {
            DrawBeam(start);
        }

        _paletteStart--;

        if (_paletteStart == -1)
        {
            _paletteStart = _palette.Length - 1;
        }
    }

    private void DrawBeam(Start start)
    {
        var x = start.X * BeamFactor + BeamFactor / 2;

        var y = start.Y * BeamFactor + BeamFactor / 2;

        var dX = start.Direction switch
        {
            Direction.West => -1,
            Direction.East => 1,
            _ => 0
        };

        var dY = start.Direction switch
        {
            Direction.North => -1,
            Direction.South => 1,
            _ => 0
        };
        
        var colorIndex = _paletteStart;

        var oldDx = dX;
        var oldDy = dY;
        
        while (x >= 0 && x < MapSize * BeamFactor && y >= 0 && y < MapSize * BeamFactor)
        {
            if ((x - BeamFactor / 2) % BeamFactor == 0 && (y - BeamFactor / 2) % BeamFactor == 0)
            {
                var mirror = _level.Mirrors.SingleOrDefault(m => m.X == x / BeamFactor && m.Y == y / BeamFactor);
                
                if (mirror != null)
                {
                    switch (mirror.Piece)
                    {
                        case '\\':
                            (dX, dY) = (dY, dX);
                            
                            break;
                        
                        case '/':
                            (dX, dY) = (-dY, -dX);

                            break;
                    }
                }
            }

            int beam = 0;
            
            if (oldDx == dX && oldDy == dY)
            {
                beam = dX == 0 ? 1 : 0;
            }
            
            oldDx = dX;
            oldDy = dY;

            _spriteBatch.Draw(_beams, 
                new Vector2(x * BeamSize, y * BeamSize), 
                new Rectangle(beam * BeamSize, 0, 7, 7), _palette[colorIndex], 
                0, Vector2.Zero, Vector2.One, SpriteEffects.None, .2f);

            colorIndex++;

            if (colorIndex == _palette.Length)
            {
                colorIndex = 0;
            }

            x += dX;
            y += dY;
        }
    }

    private void DrawPieces()
    {
        var index = 0;
        
        for (var y = 28; y >= 0; y++)
        {
            if (index >= _level.Pieces.Count)
            {
                break;
            }

            var offset = _level.Pieces[index] switch
            {
                '|' => 1,
                '\\' => 2,
                '/' => 3,
                _ => 0
            };

            _spriteBatch.Draw(_mirrors,
                new Vector2(31 * TileSize, y * TileSize),
                new Rectangle(offset * TileSize, 0, TileSize, TileSize),
                Color.DarkCyan, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, .1f);

            index++;
        }
    }

    private void DrawMirrors()
    {
        foreach (var mirror in _level.Mirrors)
        {
            var offset = mirror.Piece switch
            {
                '|' => 1,
                '\\' => 2,
                '/' => 3,
                _ => 0
            };
            
            _spriteBatch.Draw(_mirrors,
                new Vector2(mirror.X * TileSize, mirror.Y * TileSize),
                new Rectangle(offset * TileSize, 0, TileSize, TileSize),
                Color.DarkCyan, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, .1f);
        }
    }

    private void DrawStarts()
    {
        foreach (var start in _level.Starts)
        {
            _spriteBatch.Draw(_other,
                new Vector2(start.X * TileSize, start.Y * TileSize),
                new Rectangle(0, 0, TileSize, TileSize),
                Color.FromNonPremultiplied(255, 255, 255, 255), 0, Vector2.Zero, Vector2.One, SpriteEffects.None, .1f);
        }
    }

    private void DrawEnds()
    {
        foreach (var end in _level.Ends)
        {
            var spriteX = end.Direction switch
            {
                Direction.North => 1,
                Direction.South => 1,
                _ => 2
            };

            var effect = end.Direction switch
            {
                Direction.South => SpriteEffects.FlipVertically,
                Direction.West => SpriteEffects.FlipHorizontally,
                _ => SpriteEffects.None
            };
            
            _spriteBatch.Draw(_other,
                new Vector2(end.X * TileSize, end.Y * TileSize),
                new Rectangle(spriteX * TileSize, 0, TileSize, TileSize),
                Color.FromNonPremultiplied(255, 255, 255, 255), 0, Vector2.Zero, Vector2.One, effect, .1f);
        }
    }

    private void DrawBackground()
    {
        for (var y = 0; y < MapSize; y++)
        {
            for (var x = 0; x < MapSize; x++)
            {
                if (! _level.Blocked.Any(b => b.X == x && b.Y == y))
                {
                    _spriteBatch.Draw(_other,
                        new Vector2(x * TileSize, y * TileSize),
                        new Rectangle(TileSize * 3, 0, TileSize, TileSize),
                        Color.FromNonPremultiplied(255, 255, 255, 50), 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);
                }
            }

            if (y > 0 && y < MapSize - 1)
            {
                _spriteBatch.Draw(_other,
                    new Vector2((MapSize + 1) * TileSize, y * TileSize),
                    new Rectangle(TileSize * 3, 0, TileSize, TileSize),
                    Color.FromNonPremultiplied(255, 255, 255, 50), 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);            
            }
        }
    }
}