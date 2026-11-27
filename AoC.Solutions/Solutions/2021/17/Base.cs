using AoC.Solutions.Extensions;
using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2021._17;

public abstract class Base : Solution
{
    public override string Description => "Probe launch trick shot";

    protected readonly List<Point> Velocities = [];

    protected int HighestY { get; private set; }

    protected void Simulate()
    {
        var input = Input[0][(Input[0].IndexOf(':') + 2)..];

        var components = input.Split(',', StringSplitOptions.TrimEntries).Select(c => c[2..]).ToArray();

        var xRange = components[0].Split("..").Select(int.FastParse).ToArray();

        var yRange = components[1].Split("..").Select(int.FastParse).ToArray();

        var minX = xRange[0];

        var maxX = xRange[1];

        var minY = yRange[0];

        var maxY = yRange[1];

        var maxVelocityY = -minY - 1;

        var maxTime = -minY * 2;

        var lower = new int[maxTime + 1];

        var upper = new int[maxTime + 1];

        Velocities.Clear();

        HighestY = 0;

        for (var t = 1; t <= maxTime; t++)
        {
            var triangle = t * (t - 1) / 2;

            lower[t] = Math.Max(minY, (int) Math.Ceiling((minY + triangle) / (double) t));

            upper[t] = Math.Min(maxVelocityY, (int) Math.Floor((maxY + triangle) / (double) t));
        }

        var seen = new int[maxVelocityY - minY + 1];

        for (var vx = 1; vx <= maxX; vx++)
        {
            if (vx * (vx + 1) / 2 < minX)
            {
                continue;
            }

            var x = 0;

            var dx = vx;

            for (var t = 1; t <= maxTime; t++)
            {
                x += dx;

                if (dx > 0)
                {
                    dx--;
                }

                if (x > maxX)
                {
                    break;
                }

                if (x < minX)
                {
                    if (dx == 0)
                    {
                        break;
                    }

                    continue;
                }

                for (var vy = lower[t]; vy <= upper[t]; vy++)
                {
                    var index = vy - minY;

                    if (seen[index] == vx)
                    {
                        continue;
                    }

                    seen[index] = vx;

                    Velocities.Add(new Point(vx, vy));

                    if (vy > 0)
                    {
                        HighestY = Math.Max(HighestY, vy * (vy + 1) / 2);
                    }
                }
            }
        }
    }
}