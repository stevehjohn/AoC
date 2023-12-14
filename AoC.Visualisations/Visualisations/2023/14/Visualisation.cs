﻿using AoC.Solutions.Solutions._2023._14;
using AoC.Visualisations.Exceptions;
using AoC.Visualisations.Infrastructure;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;

namespace AoC.Visualisations.Visualisations._2023._14;

[UsedImplicitly]
public class Visualisation : VisualisationBase<PuzzleState>
{
    private SpriteBatch _spriteBatch;
    
    private Rock[,] _map;
    
    private PuzzleState _state;

    private Texture2D _sprites;

    public Visualisation()
    {
        GraphicsDeviceManager = new GraphicsDeviceManager(this)
                                 {
                                     PreferredBackBufferWidth = 700,
                                     PreferredBackBufferHeight = 700
                                 };

        // Something funky going on with having to add \bin\Windows - investigate.
        Content.RootDirectory = "./14";

        IgnoreQueueLimit = true;
    }

    public override void SetPart(int part)
    {
        switch (part)
        {
            case 2:
                Puzzle = new Part2(this);

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

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _sprites = Content.Load<Texture2D>("sprites");

        base.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        if (HasNextState)
        {
            var state = GetNextState();

            _map = new Rock[state.Map.GetLength(0), state.Map.GetLength(1)];

            var rng = new Random();

            var i = 0;
            
            for (var x = 0; x < state.Map.GetLength(0); x++)
            {
                for (var y = 0; y < state.Map.GetLength(1); y++)
                {
                    if (state.Map[x, y] != '.')
                    {
                        _map[x, y] = new Rock
                        {
                            Round = state.Map[x, y] == 'O',
                            Color = rng.Next(6) switch
                            {
                                0 => Color.Red,
                                1 => Color.Green,
                                2 => Color.Blue,
                                3 => Color.Yellow,
                                4 => Color.Magenta,
                                5 => Color.Cyan,
                                _ => Color.White
                            },
                            Id = i
                        };

                        i++;
                    }
                }
            }

            _state = state;
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        if (_state != null)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin(SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp);

            DrawState();
            
            _spriteBatch.End();
        }
        else
        {
            GraphicsDevice.Clear(Color.Black);
        }

        base.Draw(gameTime);
    }

    private void DrawState()
    {
        for (var y = 0; y < _map.GetLength(1); y++)
        {
            for (var x = 0; x < _map.GetLength(0); x++)
            {
                if (_map[x, y] == null)
                {
                    continue;
                }

                var sprite = _map[x, y].Round ? new Rectangle(0, 0, 7, 7) : new Rectangle(7, 0, 7, 7);

                var color = _map[x, y].Round ? _map[x, y].Color : Color.FromNonPremultiplied(80, 80, 80, 255);
                
                _spriteBatch.Draw(_sprites, new Vector2(x * 7, y * 7), sprite, color, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, .1f);
            }
        }
    }
}