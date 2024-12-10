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

                    var baseIndex = (x + y * _width) * 8;

                    for (var i = 0; i < 8; i++)
                    {
                        _vertices[baseIndex + i].Color = _palette[height - '0'];
                    }
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
                        var baseIndex = (point.X + point.Y * _width) * 8;

                        for (var i = 0; i < 8; i++)
                        {
                            _vertices[baseIndex + i].Color = Color.White;
                        }
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

        int baseIndex;

        (int X, int Y) point;
        
        if (_index == -1)
        {
            for (var x = 0; x < _width; x++)
            {
                for (var y = 0; y < _height; y++)
                {
                    var height = PuzzleState.Map[x, y];

                    point = (x, y);

                    baseIndex = (point.X + point.Y * _width) * 8;

                    for (var i = 0; i < 8; i++)
                    {
                        _vertices[baseIndex + i].Color = _palette[height - '0'];
                    }
                }
            }
        }

        _index++;

        if (_index > PuzzleState.AllVisited.Count - 1)
        {
            return;
        }

        point = PuzzleState.AllVisited[_index];
        
        baseIndex = (point.X + point.Y * _width) * 8;

        for (var i = 0; i < 8; i++)
        {
            _vertices[baseIndex + i].Color = Color.Cyan;
        }
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
        _vertices = new VertexPositionColorNormal[_width * _height * 4];
        // _outlines = new VertexPositionColorNormal[_width * _height * 8];

        for (var x = 0; x < _width; x++)
        {
            for (var y = 0; y < _height; y++)
            {
                var height = PuzzleState.Map[x, y] - '.';
                var color = _palette[PuzzleState.Map[x, y] - '0'];

                var baseIndex = (x + y * _width) * 4;

                var z = -_height / 2 + y;

                _vertices[baseIndex + 0] = new VertexPositionColorNormal(new Vector3(x, height, z), color, Vector3.Up);
                _vertices[baseIndex + 1] = new VertexPositionColorNormal(new Vector3(x + 1, height, z), color, Vector3.Up);
                _vertices[baseIndex + 2] = new VertexPositionColorNormal(new Vector3(x, height, z + 1), color, Vector3.Up);
                _vertices[baseIndex + 3] = new VertexPositionColorNormal(new Vector3(x + 1, height, z + 1), color, Vector3.Up);

                // _vertices[baseIndex + 4] = new VertexPositionColorNormal(new Vector3(x, 0, z), color, Vector3.Down);
                // _vertices[baseIndex + 5] = new VertexPositionColorNormal(new Vector3(x + 1, 0, z), color, Vector3.Down);
                // _vertices[baseIndex + 6] = new VertexPositionColorNormal(new Vector3(x, 0, z + 1), color, Vector3.Down);
                // _vertices[baseIndex + 7] = new VertexPositionColorNormal(new Vector3(x + 1, 0, z + 1), color, Vector3.Down);

                // for (var i = 0; i < 8; i++)
                // {
                //     _outlines[baseIndex + i] = new VertexPositionColorNormal(_vertices[baseIndex + i].Position, Color.Black, Vector3.Zero);
                // }
            }
        }
    }

    private void SetUpIndices()
    {
        _indices = new short[_width * _height * 36];

        var counter = 0;

        for (var x = 0; x < _width; x++)
        {
            for (var y = 0; y < _height; y++)
            {
                var baseVertex = (x + y * _width) * 8;

                _indices[counter++] = (short) (baseVertex + 0);
                _indices[counter++] = (short) (baseVertex + 1);
                _indices[counter++] = (short) (baseVertex + 2);

                _indices[counter++] = (short) (baseVertex + 1);
                _indices[counter++] = (short) (baseVertex + 3);
                _indices[counter++] = (short) (baseVertex + 2);

                for (var i = 0; i < 4; i++)
                {
                    var top1 = baseVertex + i;
                    var top2 = baseVertex + (i + 1) % 4;
                    var base1 = baseVertex + 4 + i;
                    var base2 = baseVertex + 4 + (i + 1) % 4;

                    _indices[counter++] = (short) top1;
                    _indices[counter++] = (short) base2;
                    _indices[counter++] = (short) base1;

                    _indices[counter++] = (short) top1;
                    _indices[counter++] = (short) top2;
                    _indices[counter++] = (short) base2;
                }
            }
        }
    }

    private void SetUpCamera()
    {
        var eye = new Vector3(0, 50, 40);

        var target = new Vector3(0, 0, 0);

        var up = Vector3.Up;

        _viewMatrix = Matrix.CreateLookAt(eye, target, up);

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

            GraphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, _vertices, 0, _vertices.Length, _indices, 0, _indices.Length / 3,
                VertexPositionColorNormal.VertexDeclaration);
        }

        rasterizerState = new RasterizerState();

        rasterizerState.CullMode = CullMode.None;
        rasterizerState.FillMode = FillMode.WireFrame;

        GraphicsDevice.RasterizerState = rasterizerState;

        // foreach (var pass in effect.CurrentTechnique.Passes)
        // {
        //     pass.Apply();
        //
        //     GraphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, _outlines, 0, _outlines.Length, _indices, 0, _indices.Length / 3,
        //         VertexPositionColorNormal.VertexDeclaration);
        // }
    }
}