using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2022._14;

public class PuzzleState
{
    public char[,] Map { get; }

    public List<Point> Positions { get; }

    public PuzzleState(char[,] map, List<Point> positions)
    {
        Map = new char[map.GetLength(0), map.GetLength(1)];

        Buffer.BlockCopy(map, 0, Map, 0, sizeof(char) * map.GetLength(0) * map.GetLength(1));

        Positions = positions.Select(p => new Point(p)).ToList();
    }
}