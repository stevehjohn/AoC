using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2024._14;

public abstract class Base : Solution
{
    public override string Description => "Restroom redoubt";

    protected const int Width = 101;

    protected const int Height = 103;
    
    private Robot[] _robots;
    
    protected void ParseInput()
    {
        _robots = new Robot[Input.Length];
        
        for (var i = 0; i < Input.Length; i++)
        {
            var line = Input[i];
            
            var parts = line.Split(' ');

            var robot = new Robot
            {
                Position = Point.Parse(parts[0][2..]),
                Velocity = Point.Parse(parts[1][2..])
            };

            _robots[i] = robot;
        }
    }

    protected void Simulate(int seconds)
    {
        for (var s = 0; s < seconds; s++)
        {
            for (var i = 0; i < _robots.Length; i++)
            {
                var robot = _robots[i];

                robot.Position.X += robot.Velocity.X;

                robot.Position.Y += robot.Velocity.Y;

                if (robot.Position.X < 0)
                {
                    robot.Position.X += Width;
                }

                if (robot.Position.X >= Width)
                {
                    robot.Position.X -= Width;
                }

                if (robot.Position.Y < 0)
                {
                    robot.Position.Y += Height;
                }

                if (robot.Position.Y >= Height)
                {
                    robot.Position.Y -= Height;
                }
            }
        }
    }

    protected long CountArea(int x, int y, int width, int height)
    {
        var count = 0L;
        
        for (var i = 0; i < _robots.Length; i++)
        {
            var robot = _robots[i];

            if (robot.Position.X >= x && robot.Position.X < x + width && robot.Position.Y >= y && robot.Position.Y < y + height)
            {
                count++;
            }
        }

        return count;
    }
}