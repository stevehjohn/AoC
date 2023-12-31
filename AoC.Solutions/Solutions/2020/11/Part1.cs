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

    protected override int CountOccupiedNeighbors(int x, int y)
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