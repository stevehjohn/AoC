using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2022._12;

public class PuzzleState
{
    public byte[,] Map { get; }

    public List<Point> History { get; }

    public PuzzleState(byte[,] map, List<Point> history)
    {
        Map = map;

        History = history;
    }
}