using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2020._11;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var result = RunGame(5);

        return result.ToString();
    }

    protected override int CountOccupiedNeighbors(int x, int y)
    {
        var count = 0;

        count += IsOccupiedNeighbor(x, y, -1, -1) ? 1 : 0;
        count += IsOccupiedNeighbor(x, y, 0, -1) ? 1 : 0;
        count += IsOccupiedNeighbor(x, y, 1, -1) ? 1 : 0;

        count += IsOccupiedNeighbor(x, y, -1, 0) ? 1 : 0;
        count += IsOccupiedNeighbor(x, y, 1, 0) ? 1 : 0;

        count += IsOccupiedNeighbor(x, y, -1, 1) ? 1 : 0;
        count += IsOccupiedNeighbor(x, y, 0, 1) ? 1 : 0;
        count += IsOccupiedNeighbor(x, y, 1, 1) ? 1 : 0;

        return count;
    }

    private bool IsOccupiedNeighbor(int x, int y, int dx, int dy)
    {
        while (true)
        {
            x += dx;
            y += dy;

            switch (Map[x, y])
            {
                case '.':
                    continue;
                case '#':
                    return true;
                case 'L':
                case '\0':
                    return false;
            }
        }
    }
}