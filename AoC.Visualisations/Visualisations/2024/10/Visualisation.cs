// ReSharper disable PossibleLossOfFraction

using AoC.Solutions.Solutions._2024._10;
using AoC.Visualisations.Exceptions;
using AoC.Visualisations.Infrastructure;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using VertexPositionColorNormal = AoC.Visualisations.Infrastructure.VertexPositionColorNormal;

namespace AoC.Visualisations.Visualisations._2024._10;

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
    
    private float _angle = (float) Math.PI;

    public Visualisation()
    {
        GraphicsDeviceManager = new GraphicsDeviceManager(this)
                                {
                                    PreferredBackBufferWidth = 1480,
                                    PreferredBackBufferHeight = 980
                                };

        Content.RootDirectory = "./17";

        IsMouseVisible = true;

        _palette = PaletteGenerator.GetPalette(12,
        [
            new Color(46, 27, 134),
            new Color(119, 35, 172),
            new Color(176, 83, 203),
            new Color(255, 168, 76),
            new Color(254, 211, 56),
            new Color(254, 253, 0)
        ]);
    }

    public override void SetPart(int part)
    {
        Puzzle = part switch
        {
            1 => new Part1(this),
            2 => new Part2(this),
            _ => throw new VisualisationParameterException()
        };
    }

    protected override void Update(GameTime gameTime)
    {
        _angle += 0.001f;

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
            
            for (var x = 0; x < _width; x++)
            {
                for (var y = 0; y < _height; y++)
                {
                    var height = PuzzleState.Map[x, y];

                    _vertices[x + y * _width].Color = _palette[height - '0'];
                }
            }

            if (HasNextState)
            {
                var state = GetNextState();
                
                _state = state;

                if (_state.Visited != null)
                {
                    foreach (var point in _state.Visited)
                    {
                        _vertices[point.X * 2 + point.Y * 2 * _width].Color = Color.AntiqueWhite;
                        _vertices[point.X * 2 + 1 + point.Y * 2 * _width].Color = Color.AntiqueWhite;
                        _vertices[point.X * 2 + 1 + (point.Y * 2 + 1) * _width].Color = Color.AntiqueWhite;
                        _vertices[point.X * 2 + (point.Y * 2 + 1) * _width].Color = Color.AntiqueWhite;
                    }
                }
            }
        }
        else if (_state != null)
        {
            RenderFinalPath();
        }

        if (_state != null)
        {
            DrawMap();
        }

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
                    var height = PuzzleState.Map[x, y];

                    _vertices[x + y * _width].Color = _palette[height - '0'];
                }
            }
        }

        _index++;
        
        if (_index > PuzzleState.AllVisited.Count - 1)
        {
            return;
        }
        
        var point = PuzzleState.AllVisited[_index];
        
        _vertices[point.X * 2 + point.Y * 2 * _width].Color = Color.Cyan;
        _vertices[point.X * 2 + 1 + point.Y * 2 * _width].Color = Color.Cyan;
        _vertices[point.X * 2 + 1 + (point.Y * 2 + 1) * _width].Color = Color.Cyan;
        _vertices[point.X * 2 + (point.Y * 2 + 1) * _width].Color = Color.Cyan;
    }

    private void InitialiseTerrain()
    {
        _width = PuzzleState.Map.GetLength(0);

        _height = PuzzleState.Map.GetLength(1);

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
                var height = PuzzleState.Map[x, y];

                _vertices[x + y * _width].Position = new Vector3(x, height, -_height / 2 + y);
                _vertices[x + y * _width].Color = _palette[height - '0'];
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
        _viewMatrix = Matrix.CreateLookAt(new Vector3(0, 100, 75), new Vector3(0, 40, 0), new Vector3(0, 1, 0));

        _projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, GraphicsDevice.Viewport.AspectRatio, 1f, 300.0f);
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