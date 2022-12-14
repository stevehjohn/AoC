using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2022._14;

public class PuzzleState
{
    public char[,] Map { get; }

    public Point Position { get; }

    public PuzzleState(char[,] map, Point position)
    {
        Map = new char[map.GetLength(0), map.GetLength(1)];

        Buffer.BlockCopy(map, 0, Map, 0, sizeof(char) * map.GetLength(0) * map.GetLength(1));

        Position = new Point(position);
    }
}