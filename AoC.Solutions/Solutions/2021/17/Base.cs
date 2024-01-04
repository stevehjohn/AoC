using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2021._17;

public abstract class Base : Solution
{
    public override string Description => "Probe launch trick shot";

    protected readonly List<Point> Velocities = new();

    protected int HighestY { get; private set; }

    private readonly Point _topLeft = new();

    private readonly Point _bottomRight = new();

    protected void Simulate()
    {
        var input = Input[0].Substring(Input[0].IndexOf(':') + 2);

        var components = input.Split(',', StringSplitOptions.TrimEntries).Select(c => c.Substring(2)).ToArray();

        var xRange = components[0].Split("..").Select(int.Parse).ToArray();

        var yRange = components[1].Split("..").Select(int.Parse).ToArray();

        _topLeft.X = xRange[0];

        _topLeft.Y = yRange[1];

        _bottomRight.X = xRange[1];

        _bottomRight.Y = yRange[0];

        for (var xVelocity = 0; xVelocity <= _bottomRight.X; xVelocity++)
        {
            for (var yVelocity = _bottomRight.Y; yVelocity <= -_bottomRight.Y; yVelocity++)
            {
                if (HitsTarget(xVelocity, yVelocity))
                {
                    Velocities.Add(new Point(xVelocity, yVelocity));
                }
            }
        }
    }

    private bool HitsTarget(int xVelocity, int yVelocity)
    {
        var position = new Point();

        var highestY = 0;

        while (true)
        {
            position.X += xVelocity;

            position.Y += yVelocity;

            if (position.Y > highestY)
            {
                highestY = position.Y;
            }

            xVelocity = Math.Max(xVelocity - 1, 0);

            yVelocity--;

            if (position.X >= _topLeft.X && position.X <= _bottomRight.X && position.Y <= _topLeft.Y && position.Y >= _bottomRight.Y)
            {
                if (highestY > HighestY)
                {
                    HighestY = highestY;
                }

                return true;
            }

            if (position.X > _bottomRight.X || position.Y < _bottomRight.Y)
            {
                return false;
            }
        }
    }
}