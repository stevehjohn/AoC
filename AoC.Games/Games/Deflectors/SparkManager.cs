using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AoC.Games.Games.Deflectors;

public class SparkManager : IActor
{
    private readonly int _topOffset;
    
    private readonly List<Spark> _sparks = [];

    private readonly Random _random = new();

    private Texture2D _spark;

    public SparkManager(int topOffset)
    {
        _topOffset = topOffset;
    }
    
    public void LoadContent(ContentManager contentManager)
    {
        _spark = contentManager.Load<Texture2D>("spark");
    }

    public void Add(float x, float y, int xRange, int yRange, int ticks, float yGravity, params Color[] colors)
    {
        _sparks.Add(new Spark
        {
            Position = new PointFloat(x, y),
            Vector = new PointFloat((-xRange + _random.Next(xRange * 2 + 1)) / 10f, -(_random.Next(yRange) / 10f)),
            Ticks = ticks,
            StartTicks = ticks,
            YGravity = yGravity,
            Color = colors[_random.Next(colors.Length)]
        });
    }

    public void Update()
    {
        var toRemove = new List<Spark>();

        foreach (var spark in _sparks)
        {
            spark.Ticks--;

            if (spark.Ticks < 0)
            {
                toRemove.Add(spark);

                continue;
            }

            spark.Position.X += spark.Vector.X;

            spark.Position.Y += spark.Vector.Y;

            spark.Vector.Y += spark.YGravity;
        }

        foreach (var spark in toRemove)
        {
            _sparks.Remove(spark);
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (var spark in _sparks)
        {
            spriteBatch.Draw(_spark, new Vector2(spark.Position.X, _topOffset + spark.Position.Y),
                new Rectangle(0, 0, 5, 5), spark.Color * ((float) spark.Ticks / spark.StartTicks), 0,
                Vector2.Zero, Vector2.One, SpriteEffects.None, 0.3f);
        }
    }
}