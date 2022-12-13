using AoC.Solutions.Solutions._2022._12;
using AoC.Visualisations.Exceptions;
using AoC.Visualisations.Infrastructure;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;

namespace AoC.Visualisations.Visualisations._2022._12;

[UsedImplicitly]
public class Visualisation : VisualisationBase<PuzzleState>
{
    private PuzzleState _state;

    private int _width;

    private int _height;

    private VertexPositionColor[] _vertices;

    private short[] _indices;

    private Matrix _viewMatrix;

    private Matrix _projectionMatrix;

    public Visualisation()
    {
        GraphicsDeviceManager = new GraphicsDeviceManager(this)
                                {
                                    PreferredBackBufferWidth = Constants.ScreenWidth,
                                    PreferredBackBufferHeight = Constants.ScreenHeight
                                };

        // Something funky going on with having to add \bin\Windows - investigate.
        Content.RootDirectory = "_Content\\2022\\12\\bin\\Windows";

        IsMouseVisible = true;
    }

    public override void SetPart(int part)
    {
        switch (part)
        {
            case 1:
                Puzzle = new Part1(this);

                break;
            default:
                throw new VisualisationParameterException();
        }
    }

    protected override void Update(GameTime gameTime)
    {
        _angle += 0.01f;

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
            }
        }

        DrawMap();

        base.Draw(gameTime);
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
        _vertices = new VertexPositionColor[_width * _height];

        for (var x = 0; x < _width; x++)
        {
            for (var y = 0; y < _height; y++)
            {
                _vertices[x + y * _width].Position = new Vector3(x, _state.Map[x, y], -_height / 2 + y);
                _vertices[x + y * _width].Color = Color.White;
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

    private float _angle;

    private void SetUpCamera()
    {
        _viewMatrix = Matrix.CreateLookAt(new Vector3(_width * 2f, _height * 2f, 80), new Vector3(0, 0, 0), new Vector3(0, 1, 0));

        _projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, GraphicsDevice.Viewport.AspectRatio, 1.0f, 300.0f);
    }

    private void DrawMap()
    {
        var rasterizerState = new RasterizerState();

        rasterizerState.CullMode = CullMode.None;
        rasterizerState.FillMode = FillMode.WireFrame;

        GraphicsDevice.RasterizerState = rasterizerState;

        var worldMatrix = Matrix.CreateTranslation(-_width / 2f, 0, 0) * Matrix.CreateRotationY(_angle);

        var effect = new BasicEffect(GraphicsDevice);

        effect.View = _viewMatrix;
        effect.Projection = _projectionMatrix;
        effect.World = worldMatrix;

        foreach (var pass in effect.CurrentTechnique.Passes)
        {
            pass.Apply();

            GraphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, _vertices, 0, _vertices.Length, _indices, 0, _indices.Length / 3, VertexPositionColor.VertexDeclaration);
        }
    }
}