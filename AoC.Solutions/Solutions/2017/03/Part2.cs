using AoC.Solutions.Common;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2017._03;

[UsedImplicitly]
public class Part2 : Base
{
    private static readonly Point[] Offsets = [new(-1, -1), new(0, -1), new(1, -1), new(-1, 0), new(1, 0), new(-1, 1), new(0, 1), new(1, 1)];

    public override string GetAnswer()
    {
        var result = Solve(int.Parse(Input[0]));

        return result.ToString();
    }

    private static int Solve(int target)
    {
        var spiral = new Dictionary<Point, int>
                     {
                         { new Point(0, 0), 1 }
                     };

        var position = new Point(1, 0);

        var ring = 1;

        while (true)
        {
            for (var i = 0; i <= ring * 2 - 1; i++)
            {
                var newValue = GetValueFor(position, spiral);

                spiral.Add(position, newValue);

                if (newValue > target)
                {
                    return newValue;
                }

                position = new Point(position.X, position.Y - 1);
            }
            
            position = new Point(position.X, position.Y + 1);

            for (var i = 0; i < ring * 2; i++)
            {
                position = new Point(position.X - 1, position.Y);

                var newValue = GetValueFor(position, spiral);

                spiral.Add(position, newValue);

                if (newValue > target)
                {
                    return newValue;
                }
            }

            for (var i = 0; i < ring * 2; i++)
            {
                position = new Point(position.X, position.Y + 1);

                var newValue = GetValueFor(position, spiral);

                spiral.Add(position, newValue);

                if (newValue > target)
                {
                    return newValue;
                }
            }

            for (var i = 0; i < ring * 2; i++)
            {
                position = new Point(position.X + 1, position.Y);

                var newValue = GetValueFor(position, spiral);

                spiral.Add(position, newValue);

                if (newValue > target)
                {
                    return newValue;
                }
            }

            position = new Point(position.X + 1, position.Y);

            ring++;
        }
    }

    private static int GetValueFor(Point position, Dictionary<Point, int> spiral)
    {
        var value = 0;

        foreach (var offset in Offsets)
        {
            var point = new Point(position.X + offset.X, position.Y + offset.Y);

            if (spiral.TryGetValue(point, out var cached))
            {
                value += cached;
            }
        }

        return value;
    }
}