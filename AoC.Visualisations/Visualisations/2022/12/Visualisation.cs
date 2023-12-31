using AoC.Solutions.Solutions._2022._12;
using AoC.Visualisations.Exceptions;
using AoC.Visualisations.Infrastructure;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;
using FillMode = Microsoft.Xna.Framework.Graphics.FillMode;
using PrimitiveType = Microsoft.Xna.Framework.Graphics.PrimitiveType;
using VertexPositionColorNormal = AoC.Visualisations.Infrastructure.VertexPositionColorNormal;
// ReSharper disable PossibleLossOfFraction

namespace AoC.Visualisations.Visualisations._2022._12;

[UsedImplicitly]
public class Visualisation : VisualisationBase<PuzzleState>
{
    private PuzzleState _state;

    private int _width;

    private int _height;

    private VertexPositionColorNormal[] _vertices;
    private VertexPositionColorNormal[] _outlines;

    private short[] _indices;

    private Matrix _viewMatrix;

    private Matrix _projectionMatrix;

    private readonly Color[] _palette;
    
    private float _angle;

    public Visualisation()
    {
        GraphicsDeviceManager = new GraphicsDeviceManager(this)
                                {
                                    PreferredBackBufferWidth = Constants.ScreenWidth,
                                    PreferredBackBufferHeight = Constants.ScreenHeight
                                };

        Content.RootDirectory = "./12";

        IsMouseVisible = true;

        _palette = PaletteGenerator.GetPalette(26,
                                                  new[]
                                                  {
                                                      new Color(46, 27, 134),
                                                      new Color(119, 35, 172),
                                                      new Color(176, 83, 203),
                                                      new Color(255, 168, 76),
                                                      new Color(254, 211, 56),
                                                      new Color(254, 253, 0)
                                                  });
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

    protected override void Update(GameTime gameTime)
    {
        _angle += 0.005f;

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        if (HasNextState)
        {
            if (_state == null)
            {
                _state = GetNextState();

                InitialiseTerrain();
            }
            else
            {
                _state = GetNextState();

                if (_state.History != null)
                {
                    for (var x = 0; x < _width; x++)
                    {
                        for (var y = 0; y < _height; y++)
                        {
                            var height = _state.Map[x, y];

                            _vertices[x + y * _width].Color = _palette[height];
                        }
                    }

                    foreach (var point in _state.History)
                    {
                        _vertices[point.X + point.Y * _width].Color = Color.AntiqueWhite;
                    }
                }
            }
        }
        else
        {
            RenderFinalPath();
        }

        DrawMap();

        base.Draw(gameTime);
    }

    private int _index = -1;

    private int _counter;

    private void RenderFinalPath()
    {
        if (_counter != 0)
        {
            _counter--;

            return;
        }
        
        _counter = 2;

        if (_index == -1)
        {
            for (var x = 0; x < _width; x++)
            {
                for (var y = 0; y < _height; y++)
                {
                    var height = _state.Map[x, y];

                    _vertices[x + y * _width].Color = _palette[height];
                }
            }
        }

        _index++;

        if (_index >= _state.History.Count - 1)
        {
            return;
        }

        var point = _state.History[_index];

        _vertices[point.X + point.Y * _width].Color = Color.Cyan;
    }

    private void InitialiseTerrain()
    {
        _width = _state.Map.GetLength(0);

        _height = _state.Map.GetLength(1);

        SetUpVertices();

        SetUpIndices();

        SetUpCamera();
    }

    private void SetUpVertices()
    {
        _vertices = new VertexPositionColorNormal[_width * _height];
        _outlines = new VertexPositionColorNormal[_width * _height];

        for (var x = 0; x < _width; x++)
        {
            for (var y = 0; y < _height; y++)
            {
                var height = _state.Map[x, y];

                _vertices[x + y * _width].Position = new Vector3(x, height, -_height / 2 + y);
                _vertices[x + y * _width].Color = _palette[height];
                _vertices[x + y * _width].Normal = Vector3.Right;

                _outlines[x + y * _width].Position = new Vector3(x, height, -_height / 2 + y);
                _outlines[x + y * _width].Color = Color.Black;
                _outlines[x + y * _width].Normal = Vector3.Right;
            }
        }
    }

    private void SetUpIndices()
    {
        _indices = new short[_width * _height * 6];

        var counter = 0;

        for (var y = 0; y < _height - 1; y++)
        {
            for (var x = 0; x < _width - 1; x++)
            {
                var lowerLeft = (short) (x + y * _width);
                var lowerRight = (short) (x + 1 + y * _width);
                var topLeft = (short) (x + (y + 1) * _width);
                var topRight = (short) (x + 1 + (y + 1) * _width);

                _indices[counter++] = topLeft;
                _indices[counter++] = lowerRight;
                _indices[counter++] = lowerLeft;

                _indices[counter++] = topLeft;
                _indices[counter++] = topRight;
                _indices[counter++] = lowerRight;
            }
        }
    }

    private void SetUpCamera()
    {
        // TODO: I hate magic numbers...
        _viewMatrix = Matrix.CreateLookAt(new Vector3(0, 50, 60), new Vector3(0, 0, 0), new Vector3(0, 1, 0));

        _projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, GraphicsDevice.Viewport.AspectRatio, 1.0f, 300.0f);
    }

    private void DrawMap()
    {
        var rasterizerState = new RasterizerState();

        rasterizerState.CullMode = CullMode.None;
        rasterizerState.FillMode = FillMode.Solid;

        GraphicsDevice.RasterizerState = rasterizerState;

        var worldMatrix = Matrix.CreateTranslation(-_width / 2f, 0, 0) * Matrix.CreateRotationY(_angle);

        var effect = new BasicEffect(GraphicsDevice);

        effect.View = _viewMatrix;
        effect.Projection = _projectionMatrix;
        effect.World = worldMatrix;
        effect.VertexColorEnabled = true;

        foreach (var pass in effect.CurrentTechnique.Passes)
        {
            pass.Apply();

            GraphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, _vertices, 0, _vertices.Length, _indices, 0, _indices.Length / 3, VertexPositionColorNormal.VertexDeclaration);
        }

        rasterizerState = new RasterizerState();

        rasterizerState.CullMode = CullMode.None;
        rasterizerState.FillMode = FillMode.WireFrame;

        GraphicsDevice.RasterizerState = rasterizerState;

        foreach (var pass in effect.CurrentTechnique.Passes)
        {
            pass.Apply();

            GraphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, _outlines, 0, _outlines.Length, _indices, 0, _indices.Length / 3, VertexPositionColorNormal.VertexDeclaration);
        }
    }
}