using AoC.Solutions.Solutions._2020._20;
using AoC.Visualisations.Exceptions;
using AoC.Visualisations.Infrastructure;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;

namespace AoC.Visualisations.Visualisations._2020._20;

[UsedImplicitly]
public class Visualisation : VisualisationBase<PuzzleState>
{
    // ReSharper disable once NotAccessedField.Local
    private readonly GraphicsDeviceManager _graphicsDeviceManager;

    private SpriteBatch _spriteBatch;

    private PuzzleState _state;

    private Part1 _preVisualisationPuzzle;

    private bool _needNewState = true;

    private List<Tile> _imageSegments;

    private TileQueue _tileQueue;

    private Jigsaw _jigsaw;

    private Texture2D _image;

    private Texture2D _queueCell;

    private Texture2D _jigsawMat;

    public Visualisation()
    {
        _graphicsDeviceManager = new GraphicsDeviceManager(this)
                                 {
                                     PreferredBackBufferWidth = Constants.ScreenWidth,
                                     PreferredBackBufferHeight = Constants.ScreenHeight
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

        var segmentCalculator = new TileCoordinatesCalculator(_preVisualisationPuzzle);

        segmentCalculator.CalculateTileCoordinatesInImage();

        _imageSegments = segmentCalculator.ImageSegments;

        base.Initialize();
    }

    protected override void BeginRun()
    {
        _tileQueue = new TileQueue(_imageSegments, _image, _queueCell);

        _jigsaw = new Jigsaw(_imageSegments, _image, _jigsawMat);

        base.BeginRun();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _image = Content.Load<Texture2D>("image");

        _queueCell = Content.Load<Texture2D>("queue-cell");

        _jigsawMat = Content.Load<Texture2D>("jigsaw-mat");

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
            GraphicsDevice.Clear(Color.Black);

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
        _tileQueue.Draw(_spriteBatch);

        _jigsaw.Draw(_spriteBatch);
    }
}