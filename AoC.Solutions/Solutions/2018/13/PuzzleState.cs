using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2018._13;

public class PuzzleState
{
    public char[,] Map { get; set; }

    public List<(Point Position, Point Direction)> Carts { get; set; }
}