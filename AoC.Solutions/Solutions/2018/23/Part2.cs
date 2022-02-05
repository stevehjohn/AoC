using AoC.Solutions.Common;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2018._23;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var result = FindOptimalPosition();

        return GetManhattanDistance(new Point(0, 0), result).ToString();
    }

    private Point FindOptimalPosition()
    {
        var min = new Point(Nanobots.Min(b => b.Position.X), Nanobots.Min(b => b.Position.Y), Nanobots.Min(b => b.Position.Z));

        var max = new Point(Nanobots.Max(b => b.Position.X), Nanobots.Max(b => b.Position.Y), Nanobots.Max(b => b.Position.Z));

        var size = Math.Min(Math.Min(max.X - min.X, max.Y - min.Y), max.Z);

        Point optimal = null;

        while (size > 0)
        {
            var maxInRange = 0;

            for (var x = min.X; x <= max.X; x += size)
            {
                for (var y = min.Y; y <= max.Y; y += size)
                {
                    for (var z = min.Z; z <= max.Z; z += size)
                    {
                        var inRange = BotsInRange(new Point(x, y, z), size);

                        if (inRange > maxInRange)
                        {
                            maxInRange = inRange;

                            optimal = new Point(x, y, z);

                            continue;
                        }
                        
                        if (maxInRange == inRange)
                        {
                            if (optimal == null || GetManhattanDistance(new Point(0, 0), new Point(x, y, z)) < GetManhattanDistance(new Point(0, 0), optimal))
                            {
                                optimal = new Point(x, y, z);
                            }
                        }
                    }
                }
            }

            if (optimal == null)
            {
                throw new NullReferenceException("This shouldn't be possible.");
            }

            min.X = optimal.X - size;

            min.Y = optimal.Y - size; 
            
            min.Z = optimal.Z - size;

            max.X = optimal.X + size;

            max.Y = optimal.Y + size; 
            
            max.Z = optimal.Z + size;

            size /= 2;
        }

        return optimal;
    }

    private int BotsInRange(Point position, int size)
    {
        var inRange = 0;

        foreach (var bot in Nanobots)
        {
            if (GetManhattanDistance(position, bot.Position) - bot.Range < size)
            {
                inRange++;
            }
        }

        return inRange;
    }
}