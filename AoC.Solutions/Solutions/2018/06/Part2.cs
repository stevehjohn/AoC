using AoC.Solutions.Common;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2018._06;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var result = MapClosestPoints();

        return result.ToString();
    }

    private int MapClosestPoints()
    {
        var count = 0;

        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                var position = new Point(x, y);

                var totalDistance = 0;

                for (var i = 0; i < Points.Length; i++)
                {
                    totalDistance += ManhattanDistance(Points[i], position);
                }

                if (totalDistance < 10_000)
                {
                    count++;
                }
            }
        }

        return count;
    }
}