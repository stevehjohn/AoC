using AoC.Solutions.Solutions._2020._20;
using AoC.Visualisations.Exceptions;
using AoC.Visualisations.Infrastructure;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;
using Point = Microsoft.Xna.Framework.Point;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace AoC.Visualisations.Visualisations._2020._20;

[UsedImplicitly]
public class Visualisation : VisualisationBase<PuzzleState>
{
    private const int JigsawSize = 12;

    private const int TileSize = 75;

    private const int TileSpacing = 5;

    private const int MovementStep = 5;

    private const int Width = 2198;

    private const int Height = 1080;

    // ReSharper disable once NotAccessedField.Local
    private readonly GraphicsDeviceManager _graphicsDeviceManager;

    private SpriteBatch _spriteBatch;

    private PuzzleState _state;

    private Part1 _preVisualisationPuzzle;

    private Texture2D _image;

    private Texture2D _scanner;

    private bool _needNewState = true;

    private readonly Dictionary<int, Tile> _tiles = new();

    private Point _scannerPosition;

    private int? _transformTileId;

    private readonly Queue<int> _transformQueue = new();

    private int _transformPhase;

    public Visualisation()
    {
        _graphicsDeviceManager = new GraphicsDeviceManager(this)
                                 {
                                     PreferredBackBufferWidth = Width,
                                     PreferredBackBufferHeight = Height
                                 };

        // Something funky going on with having to add \bin\Windows - investigate.
        Content.RootDirectory = "_Content\\2020\\20\\bin\\Windows";
    }

    public override void SetPart(int part)
    {
        switch (part)
        {
            case 1:
                Puzzle = new Part1(this);

                _preVisualisationPuzzle = new Part1();

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

    protected override void BeginRun()
    {
        CalculateImageSegments();

        _scannerPosition = GetQueuedTilePosition(0, 0);

        base.BeginRun();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _image = Content.Load<Texture2D>("image");

        _scanner = Content.Load<Texture2D>("scanner");

        base.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        if (HasNextState && _needNewState)
        {
            _state = GetNextState();
        }

        _needNewState = false;

        Update();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        if (_state != null)
        {
            GraphicsDevice.Clear(Color.Black); // TODO: Pick a better colour/background.

            _spriteBatch.Begin(SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp);

            Draw();

            _spriteBatch.End();
        }
        else
        {
            GraphicsDevice.Clear(Color.Black);
        }

        base.Draw(gameTime);
    }

    private void Update()
    {
        UpdateScan();

        UpdateTransformTile();
    }

    private void UpdateTransformTile()
    {
        if (_transformTileId == null && _transformQueue.Count > 0)
        {
            _transformTileId = _transformQueue.Dequeue();

            _transformPhase = 0;
        }

        if (_transformTileId != null)
        {
            if (_transformPhase == 0)
            {
                var x = _tiles[_transformTileId.Value].ScreenPosition.X;

                var y = _tiles[_transformTileId.Value].ScreenPosition.Y;

                x += Math.Sign(Width / 2 - _tiles[_transformTileId.Value].ScreenPosition.X) * MovementStep;

                y += Math.Sign(Height / 2 - _tiles[_transformTileId.Value].ScreenPosition.Y) * MovementStep;

                if (Math.Abs(x - Width / 2) < MovementStep)
                {
                    x = Width / 2;
                }

                if (Math.Abs(y - Height / 2) < MovementStep)
                {
                    y = Height / 2;
                }

                _tiles[_transformTileId.Value].ScreenPosition = new Point(x, y);

                if (x == Width / 2 && y == Height / 2)
                {
                    _transformPhase = 1;
                }
            }
        }
    }

    private void UpdateScan()
    {
        var target = _tiles[_state.TileId];

        if (target.ScreenPosition.X == _scannerPosition.X && target.ScreenPosition.Y == _scannerPosition.Y && _transformQueue.Count < 1)
        {
            _transformQueue.Enqueue(_state.TileId);

            _needNewState = true;

            return;
        }

        _scannerPosition = new Point(_scannerPosition.X + Math.Sign(target.ScreenPosition.X - _scannerPosition.X) * MovementStep,
                                     _scannerPosition.Y + Math.Sign(target.ScreenPosition.Y - _scannerPosition.Y) * MovementStep);
    }

    private void Draw()
    {
        foreach (var tile in _tiles)
        {
            DrawTile(tile.Value);
        }

        _spriteBatch.Draw(_scanner,
                          new Vector2(_scannerPosition.X - TileSpacing, _scannerPosition.Y - TileSpacing),
                          new Rectangle(0, 0, 85, 85),
                          Color.White,
                          0,
                          new Vector2(TileSize / 2f, TileSize / 2f),
                          Vector2.One,
                          SpriteEffects.None,
                          1);


        if (_transformTileId != null)
        {
            var tile = _tiles[_transformTileId.Value];

            DrawTile(tile);
        }
    }

    private void DrawTile(Tile tile)
    {
        var spriteEffects = tile.Transform.Contains('H') ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

        spriteEffects |= tile.Transform.Contains('V') ? SpriteEffects.FlipVertically : SpriteEffects.None;

        var rotation = Math.PI / 2 * tile.Transform.Count(c => c == 'R');

        _spriteBatch.Draw(_image,
                          new Vector2(tile.ScreenPosition.X, tile.ScreenPosition.Y),
                          new Rectangle(tile.ImageSegment.X, tile.ImageSegment.Y, tile.ImageSegment.Width, tile.ImageSegment.Height),
                          Color.White,
                          (float) rotation,
                          new Vector2(TileSize / 2f, TileSize / 2f),
                          Vector2.One,
                          spriteEffects,
                          1);
    }

    private void CalculateImageSegments()
    {
        _preVisualisationPuzzle.GetAnswer();

        var jigsaw = _preVisualisationPuzzle.Jigsaw;

        var oX = jigsaw.Min(t => t.Key.X);

        var oY = jigsaw.Min(t => t.Key.Y);

        var tilePositions = _preVisualisationPuzzle.Jigsaw.Select(t => (t.Value.Id, Position: new Point(t.Key.X - oX, t.Key.Y - oY))).ToList();

        int x = 0, y = 0;

        var transforms = _preVisualisationPuzzle.Transforms;

        var initialOrder = _preVisualisationPuzzle.InitialTileOrder;

        (int Id, Point Position) jigsawTile;

        Tile tile;

        foreach (var id in initialOrder.Skip(1))
        {
            jigsawTile = tilePositions.Single(p => p.Id == id);

            if (! transforms.TryGetValue(jigsawTile.Id, out var transform))
            {
                transform = string.Empty;
            }

            tile = new Tile
                   {
                       ScreenPosition = GetQueuedTilePosition(x, y),
                       ImageSegment = new Rectangle(jigsawTile.Position.X * TileSize, jigsawTile.Position.Y * TileSize, TileSize, TileSize),
                       Transform = transform
                   };

            _tiles.Add(id, tile);

            x++;

            if (x > 11)
            {
                x = 0;

                y++;
            }
        }

        jigsawTile = tilePositions.Single(p => p.Id == initialOrder.First());

        tile = new Tile
               {
                   ScreenPosition = new Point(Width / 4, Height / 2),
                   ImageSegment = new Rectangle(jigsawTile.Position.X * TileSize, jigsawTile.Position.Y * TileSize, TileSize, TileSize),
                   Transform = string.Empty
               };

        _tiles.Add(jigsawTile.Id, tile);
    }

    private Point GetQueuedTilePosition(int x, int y)
    {
        var top = Height / 2 - (TileSize + TileSpacing) * JigsawSize / 2 + TileSize / 2;

        var left = Width / 2 + TileSize * 2;

        return new Point(left + x * (TileSize + TileSpacing), top + TileSpacing + y * (TileSize + TileSpacing));
    }
}