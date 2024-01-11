using AoC.Games.Games.Deflectors.Levels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AoC.Games.Games.Deflectors;

public class ArenaManager : IActor
{
    public const int MapSize = 30;

    private const int TileSize = 21;

    private readonly int _topOffset;

    private readonly Input _input;

    private readonly LevelDataProvider _levelDataProvider;
    
    private Texture2D _mirrors;

    private Texture2D _other;

    private int _levelNumber;

    private (int X, int Y) _mirrorPosition;
    
    public Level Level { get; private set; }

    public (int X, int Y) LastMirrorPosition { get; private set; } = (-1, -1);

    public (int X, int Y) MirrorPosition => _mirrorPosition;

    public char Mirror { get; private set; }

    public int LevelCount => _levelDataProvider.LevelCount;

    public int LevelNumber => _levelNumber;
    
    public ArenaManager(int topOffset, Input input)
    {
        _topOffset = topOffset;

        _levelDataProvider = new LevelDataProvider();

        _input = input;
        
        SetLevel(1);
    }
    
    public void LoadContent(ContentManager contentManager)
    {
        _mirrors = contentManager.Load<Texture2D>("mirrors");

        _other = contentManager.Load<Texture2D>("other");
    }

    public void SetLevel(int levelNumber)
    {
        _levelNumber = levelNumber;
        
        Level = _levelDataProvider.GetLevel(levelNumber);

        Mirror = Level.Pieces[0];
        
        Level.Pieces.RemoveAt(0);
    }

    public void ResetLevel()
    {
        SetLevel(_levelNumber);
    }

    public void NextLevel()
    {
        _levelNumber++;

        if (_levelNumber > _levelDataProvider.LevelCount)
        {
            _levelNumber = 1;
        }

        SetLevel(_levelNumber);
    }

    public void PlaceMirror()
    {
        if (MirrorPosition == (-1, -1))
        {
            return;
        }

        if (Level.Mirrors.Any(m => m.X == MirrorPosition.X && m.Y == MirrorPosition.Y)
            || Level.Blocked.Any(b => b.X == MirrorPosition.X && b.Y == MirrorPosition.Y)
            || Level.Starts.Any(s => s.X == MirrorPosition.X && s.Y == MirrorPosition.Y)
            || Level.Ends.Any(e => e.X == MirrorPosition.X && e.Y == MirrorPosition.Y))
        {
            return;
        }

        Level.Mirrors.Add(new Mirror
        {
            Piece = Mirror,
            Placed = true,
            X = MirrorPosition.X,
            Y = MirrorPosition.Y
        });

        Mirror = '\0';

        if (Level.Pieces.Count > 0)
        {
            Mirror = Level.Pieces[0];

            Level.Pieces.RemoveAt(0);
        }
    }
    
    public void Update()
    {
        LastMirrorPosition = _mirrorPosition;

        var position = (X: _input.MouseX, Y: _input.MouseY);

        if (position.X >= 0 && position.X < MapSize * TileSize && position.Y >= 0 && position.Y < MapSize * TileSize && Mirror != '\0')
        {
            _mirrorPosition = (position.X / TileSize, position.Y / TileSize);
        }
        else
        {
            _mirrorPosition = (-1, -1);
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        DrawBackground(spriteBatch);
        
        DrawPieces(spriteBatch);

        DrawMirrors(spriteBatch);
        
        DrawStarts(spriteBatch);
        
        DrawEnds(spriteBatch);
    }

    private void DrawMirrors(SpriteBatch spriteBatch)
    {
        foreach (var mirror in Level.Mirrors)
        {
            var offset = mirror.Piece switch
            {
                '|' => 1,
                '\\' => 2,
                '/' => 3,
                _ => 0
            };

            spriteBatch.Draw(_mirrors,
                new Vector2(mirror.X * TileSize, _topOffset + mirror.Y * TileSize),
                new Rectangle(offset * TileSize, 0, TileSize, TileSize),
                mirror.Placed ? Color.Green : Color.DarkCyan, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, .4f);
        }

        if (MirrorPosition != (-1, -1))
        {
            var offset = Mirror switch
            {
                '|' => 1,
                '\\' => 2,
                '/' => 3,
                _ => 0
            };

            var color = Color.FromNonPremultiplied(0, 255, 0, 255);

            if (Level.Mirrors.Any(m => m.X == MirrorPosition.X && m.Y == MirrorPosition.Y)
                || Level.Blocked.Any(b => b.X == MirrorPosition.X && b.Y == MirrorPosition.Y)
                || Level.Starts.Any(s => s.X == MirrorPosition.X && s.Y == MirrorPosition.Y)
                || Level.Ends.Any(e => e.X == MirrorPosition.X && e.Y == MirrorPosition.Y))
            {
                color = Color.Red;
            }

            spriteBatch.Draw(_mirrors,
                new Vector2(MirrorPosition.X * TileSize, _topOffset + MirrorPosition.Y * TileSize),
                new Rectangle(offset * TileSize, 0, TileSize, TileSize),
                color, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, .5f);
        }
    }

    private void DrawStarts(SpriteBatch spriteBatch)
    {
        foreach (var start in Level.Starts)
        {
            spriteBatch.Draw(_other,
                new Vector2(start.X * TileSize, _topOffset + start.Y * TileSize),
                new Rectangle(0, 0, TileSize, TileSize),
                Color.FromNonPremultiplied(255, 255, 255, 255), 0, Vector2.Zero, Vector2.One, SpriteEffects.None, .1f);
        }
    }

    private void DrawEnds(SpriteBatch spriteBatch)
    {
        foreach (var end in Level.Ends)
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

            spriteBatch.Draw(_other,
                new Vector2(end.X * TileSize, _topOffset + end.Y * TileSize),
                new Rectangle(spriteX * TileSize, 0, TileSize, TileSize),
                Color.FromNonPremultiplied(255, 255, 255, 255), 0, Vector2.Zero, Vector2.One, effect, .1f);
        }
    }

    private void DrawBackground(SpriteBatch spriteBatch)
    {
        for (var y = 0; y < MapSize; y++)
        {
            for (var x = 0; x < MapSize; x++)
            {
                if (! Level.Blocked.Any(b => b.X == x && b.Y == y))
                {
                    spriteBatch.Draw(_other,
                        new Vector2(x * TileSize, _topOffset + y * TileSize),
                        new Rectangle(TileSize * 3, 0, TileSize, TileSize),
                        Color.FromNonPremultiplied(255, 255, 255, 25), 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);
                }
                else
                {
                    spriteBatch.Draw(_other,
                        new Vector2(x * TileSize, _topOffset + y * TileSize),
                        new Rectangle(TileSize * 3, 0, TileSize, TileSize),
                        Color.FromNonPremultiplied(255, 255, 255, 200), 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);
                }
            }

            if (y > 0 && y <= 10)
            {
                spriteBatch.Draw(_other,
                    new Vector2((MapSize + 1) * TileSize, _topOffset + y * TileSize),
                    new Rectangle(TileSize * 3, 0, TileSize, TileSize),
                    Color.FromNonPremultiplied(255, 255, 255, 25), 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);
            }
        }
    }

    private void DrawPieces(SpriteBatch spriteBatch)
    {
        var index = 0;

        for (var y = 10; y > 0; y--)
        {
            if (index >= Level.Pieces.Count)
            {
                break;
            }

            var offset = Level.Pieces[index] switch
            {
                '|' => 1,
                '\\' => 2,
                '/' => 3,
                _ => 0
            };

            spriteBatch.Draw(_mirrors,
                new Vector2(31 * TileSize, _topOffset + y * TileSize),
                new Rectangle(offset * TileSize, 0, TileSize, TileSize),
                Color.Cyan, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, .1f);

            index++;
        }
    }
}