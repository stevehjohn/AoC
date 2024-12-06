using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._06;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var count = 0;

        WalkMap();
        
        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                if (Map[x, y] == '#' || (x == StartPosition.X && y == StartPosition.Y))
                {
                    continue;
                }

                if (! Visited.Contains(x + y * Width))
                {
                    continue;
                }

                Map[x, y] = '#';
                
                var result = WalkMap(true);

                if (result == -1)
                {
                    count++;
                }

                Map[x, y] = '.';
            }
        }

        return count.ToString();
    }
}