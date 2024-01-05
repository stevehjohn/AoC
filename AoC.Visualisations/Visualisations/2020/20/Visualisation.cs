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
    private const int Transformers = 3;

    private readonly List<Transformer> _transformers = [];

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
    
    private Texture2D _jigsawMatBorder;

    private Texture2D _scanHighlight;

    public Visualisation()
    {
        GraphicsDeviceManager = new GraphicsDeviceManager(this)
                                 {
                                     PreferredBackBufferWidth = Constants.ScreenWidth,
                                     PreferredBackBufferHeight = Constants.ScreenHeight
                                 };

        Content.RootDirectory = "./20";
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
        _tileQueue = new TileQueue(_imageSegments, _image, _queueCell, _scanHighlight);
        
        _tileQueue.StartScan(-1);

        _jigsaw = new Jigsaw(_image, _jigsawMat, _jigsawMatBorder);


        var y = -300;

        for (var i = 0; i < Transformers; i ++)
        {
            _transformers.Add(new Transformer(_image, _queueCell, _jigsaw, y));

            y += 300;
        }

        for (var i = 0; i < Transformers; i++)
        {
            _transformers[i].OtherTransformers = _transformers.Where(t => t != _transformers[i]).ToList();
        }

        base.BeginRun();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _image = Content.Load<Texture2D>("image");

        _queueCell = Content.Load<Texture2D>("queue-cell");

        _jigsawMat = Content.Load<Texture2D>("jigsaw-mat");

        _jigsawMatBorder = Content.Load<Texture2D>("jigsaw-mat-border");

        _scanHighlight = Content.Load<Texture2D>("scan-highlight");

        base.LoadContent();
    }

    protected override bool VisualisationFinished => _tileQueue.IsEmpty;

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
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin(SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp);

        Draw();

        _spriteBatch.End();

        base.Draw(gameTime);
    }

    private void Update()
    {
        _tileQueue.Update();

        _jigsaw.Update();

        for (var i = 0; i < Transformers; i++)
        {
            _transformers[i].Update();
        }

        if (_jigsaw.CanTakeTile)
        {
            Tile tile = null;

            for (var i = 0; i < Transformers; i++)
            {
                tile = _transformers[i].TransformedTile;

                if (tile != null)
                {
                    break;
                }
            }

            if (tile != null)
            {
                _jigsaw.AddTile(tile);
            }
        }

        for (var i = 0; i < Transformers; i++)
        {
            if (_transformers[i].CanTakeTile)
            {
                var matchedTile = _tileQueue.MatchedTile;

                if (matchedTile != null)
                {
                    _transformers[i].AddTile(matchedTile.Value.Tile, matchedTile.Value.ScreenPosition);
                }
            }
        }

        if (_state != null)
        {
            _tileQueue.StartScan(_state.TileId);

            _state = null;

            _needNewState = true;
        }
    }

    private void Draw()
    {
        _tileQueue.Draw(_spriteBatch);

        _jigsaw.Draw(_spriteBatch);

        for (var i = 0; i < Transformers; i++)
        {
            _transformers[i].Draw(_spriteBatch);
        }
    }
}