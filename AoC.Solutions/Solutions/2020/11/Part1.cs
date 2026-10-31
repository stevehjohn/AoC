using AoC.Solutions.Common;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2020._11;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var result = RunGame(4);

        return result.ToString();
    }

    private int RunGame(int vacateQuantity)
    {
        var flips = new List<Point>();

        do
        {
            flips.Clear();

            for (var y = 1; y <= Height; y++)
            {
                for (var x = 1; x <= Width; x++)
                {
                    if (Map[x, y] == '.')
                    {
                        continue;
                    }

                    var occupiedNeighbors = CountOccupiedNeighbors(x, y);

                    if (Map[x, y] == 'L')
                    {
                        if (occupiedNeighbors == 0)
                        {
                            flips.Add(new Point(x, y));
                        }

                        continue;
                    }

                    if (occupiedNeighbors >= vacateQuantity)
                    {
                        flips.Add(new Point(x, y));
                    }
                }
            }

            foreach (var flip in flips)
            {
                Map[flip.X, flip.Y] = Map[flip.X, flip.Y] == '#' ? 'L' : '#';
            }
        } while (flips.Count > 0);

        var occupied = 0;

        for (var y = 1; y <= Height; y++)
        {
            for (var x = 1; x <= Width; x++)
            {
                occupied += Map[x, y] == '#' ? 1 : 0;
            }
        }

        return occupied;
    }

    private  int CountOccupiedNeighbors(int x, int y)
    {
        var count = 0;

        count += Map[x - 1, y - 1] == '#' ? 1 : 0;
        count += Map[x, y - 1] == '#' ? 1 : 0;
        count += Map[x + 1, y - 1] == '#' ? 1 : 0;

        count += Map[x - 1, y] == '#' ? 1 : 0;
        count += Map[x + 1, y] == '#' ? 1 : 0;

        count += Map[x - 1, y + 1] == '#' ? 1 : 0;
        count += Map[x, y + 1] == '#' ? 1 : 0;
        count += Map[x + 1, y + 1] == '#' ? 1 : 0;

        return count;
    }
}