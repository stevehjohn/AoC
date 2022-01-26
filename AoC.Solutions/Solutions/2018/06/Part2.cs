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

                // This could probably be sped up by: https://www.youtube.com/watch?v=BK5x7IUTIyU&t=666s
                var distances = Points.Select(p => ManhattanDistance(p, position));

                if (distances.Sum() < 10_000)
                {
                    count++;
                }
            }
        }

        return count;
    }
}