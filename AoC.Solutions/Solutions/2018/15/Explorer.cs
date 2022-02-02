using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2018._15;

public class Explorer
{
    private readonly bool[,] _map;

    public Explorer(bool[,] map, List<Unit> units)
    {
        _map = new bool[map.GetLength(0), map.GetLength(1)];

        Buffer.BlockCopy(map, 0, _map, 0, map.GetLength(0) * map.GetLength(1) * sizeof(bool));

        foreach (var unit in units)
        {
            _map[unit.Position.X, unit.Position.Y] = true;
        }
    }

    public bool CanReach(Point a, Point b)
    {
        return true;
    }
}