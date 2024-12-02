using AoC.Games.Games.Deflectors.Levels;
using AoC.Games.Infrastructure;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AoC.Games.Games.Deflectors.Actors;

public class ArenaManager : IActor
{
    private readonly Input _input;

    private readonly LevelDataProvider _levelDataProvider;
    
    private Texture2D _mirrors;

    private Texture2D _other;

    public Level Level { get; private set; }

    public (int X, int Y) LastMirrorPosition { get; private set; } = (-1, -1);

    public (int X, int Y) MirrorPosition { get; private set; }

    public char Mirror { get; private set; }

    public int LevelCount => _levelDataProvider.LevelCount;

    public int LevelNumber { get; private set; }

    public ArenaManager(Input input)
    {
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
        LevelNumber = levelNumber;
        
        Level = _levelDataProvider.GetLevel(levelNumber);

        Mirror = Level.Pieces[0];
        
        Level.Pieces.RemoveAt(0);
    }

    public void ResetLevel()
    {
        SetLevel(LevelNumber);
    }

    public void NextLevel()
    {
        LevelNumber++;

        if (LevelNumber > _levelDataProvider.LevelCount)
        {
            LevelNumber = 1;
        }

        SetLevel(LevelNumber);
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
        LastMirrorPosition = MirrorPosition;

        var scaleFactor = AppSettings.Instance.ScaleFactor;
        
        var position = (X: (int) (_input.MouseX / scaleFactor), Y: (int) (_input.MouseY / scaleFactor));

        if (position.X is >= 0 and < Constants.MapSize * Constants.TileSize && position.Y is >= Constants.TopOffset and < Constants.MapSize * Constants.TileSize + Constants.TopOffset && Mirror != '\0')
        {
            MirrorPosition = (position.X / Constants.TileSize, (position.Y - Constants.TopOffset) / Constants.TileSize);
        }
        else
        {
            MirrorPosition = (-1, -1);
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
                new Vector2(mirror.X * Constants.TileSize, Constants.TopOffset + mirror.Y * Constants.TileSize),
                new Rectangle(offset * Constants.TileSize, 0, Constants.TileSize, Constants.TileSize),
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
                new Vector2(MirrorPosition.X * Constants.TileSize, Constants.TopOffset + MirrorPosition.Y * Constants.TileSize),
                new Rectangle(offset * Constants.TileSize, 0, Constants.TileSize, Constants.TileSize),
                color, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, .5f);
        }
    }

    private void DrawStarts(SpriteBatch spriteBatch)
    {
        foreach (var start in Level.Starts)
        {
            spriteBatch.Draw(_other,
                new Vector2(start.X * Constants.TileSize, Constants.TopOffset + start.Y * Constants.TileSize),
                new Rectangle(0, 0, Constants.TileSize, Constants.TileSize),
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
                new Vector2(end.X * Constants.TileSize, Constants.TopOffset + end.Y * Constants.TileSize),
                new Rectangle(spriteX * Constants.TileSize, 0, Constants.TileSize, Constants.TileSize),
                Color.FromNonPremultiplied(255, 255, 255, 255), 0, Vector2.Zero, Vector2.One, effect, .1f);
        }
    }

    private void DrawBackground(SpriteBatch spriteBatch)
    {
        for (var y = 0; y <Constants. MapSize; y++)
        {
            for (var x = 0; x < Constants.MapSize; x++)
            {
                spriteBatch.Draw(_other,
                    new Vector2(x * Constants.TileSize, Constants.TopOffset + y * Constants.TileSize),
                    new Rectangle(Constants.TileSize * 3, 0, Constants.TileSize, Constants.TileSize),
                    Level.Blocked.Any(b => b.X == x && b.Y == y)
                        ? Color.FromNonPremultiplied(255, 255, 255, 200)
                        : Color.FromNonPremultiplied(255, 255, 255, 25), 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);
            }

            if (y is > 0 and <= 10)
            {
                spriteBatch.Draw(_other,
                    new Vector2((Constants.MapSize + 1) * Constants.TileSize, Constants.TopOffset + y * Constants.TileSize),
                    new Rectangle(Constants.TileSize * 3, 0, Constants.TileSize, Constants.TileSize),
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
                new Vector2(31 * Constants.TileSize, Constants.TopOffset + y * Constants.TileSize),
                new Rectangle(offset * Constants.TileSize, 0, Constants.TileSize, Constants.TileSize),
                Color.Cyan, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, .1f);

            index++;
        }
    }
}