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

    // ReSharper disable once NotAccessedField.Local
    private readonly GraphicsDeviceManager _graphicsDeviceManager;

    private SpriteBatch _spriteBatch;

    private PuzzleState _state;

    private Part1 _preVisualisationPuzzle;

    private Dictionary<int, string> _transforms;

    private Texture2D _image;

    private bool _needNewState = true;

    private readonly Dictionary<int, Tile> _tiles = new();

    public Visualisation()
    {
        _graphicsDeviceManager = new GraphicsDeviceManager(this)
                                 {
                                     PreferredBackBufferWidth = 2048,
                                     PreferredBackBufferHeight = 1080
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

        base.BeginRun();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _image = Content.Load<Texture2D>("image");

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
            GraphicsDevice.Clear(Color.Gray); // TODO: Pick a better colour.

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
    }

    private void Draw()
    {
        foreach (var tile in _tiles)
        {
            var spriteEffects = tile.Value.Transform.Contains('H') ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            spriteEffects |= tile.Value.Transform.Contains('V') ? SpriteEffects.FlipVertically : SpriteEffects.None;

            var rotation = Math.PI / 2 * tile.Value.Transform.Count(c => c == 'R');

            _spriteBatch.Draw(_image,
                              new Vector2(tile.Value.ScreenPosition.X, tile.Value.ScreenPosition.Y),
                              new Rectangle(tile.Value.ImageSegment.X, tile.Value.ImageSegment.Y, tile.Value.ImageSegment.Width, tile.Value.ImageSegment.Height),
                              Color.White,
                              (float) rotation,
                              new Vector2(TileSize / 2f, TileSize / 2f),
                              Vector2.One,
                              spriteEffects,
                              1);
        }
    }

    private void CalculateImageSegments()
    {
        _preVisualisationPuzzle.GetAnswer();

        var jigsaw = _preVisualisationPuzzle.Jigsaw;

        var oX = jigsaw.Min(t => t.Key.X);

        var oY = jigsaw.Min(t => t.Key.Y);

        var tilePositions = _preVisualisationPuzzle.Jigsaw.Select(t => (t.Value.Id, Position: new Point(t.Key.X - oX, t.Key.Y - oY))).ToList();

        var top = _graphicsDeviceManager.PreferredBackBufferHeight / 2 - (TileSize + TileSpacing) * JigsawSize / 2 + TileSize / 2;

        var i = 0;

        var transforms = _preVisualisationPuzzle.Transforms;

        for (var y = 0; y < JigsawSize; y++)
        {
            for (var x = 0; x < JigsawSize; x++)
            {
                var jigsawTile = tilePositions[i];

                if (! transforms.TryGetValue(jigsawTile.Id, out var transform))
                {
                    transform = string.Empty;
                }

                var tile = new Tile
                           {
                               ScreenPosition = new Point(_graphicsDeviceManager.PreferredBackBufferWidth - (x + 1) * (TileSize + TileSpacing), top + TileSpacing + y * (TileSize + TileSpacing)),
                               ImageSegment = new Rectangle(jigsawTile.Position.X * TileSize, jigsawTile.Position.Y * TileSize, TileSize, TileSize),
                               Transform = transform
                           };

                _tiles.Add(jigsawTile.Id, tile);

                i++;
            }
        }
    }
}