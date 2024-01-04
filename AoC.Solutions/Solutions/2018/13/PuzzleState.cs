using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2018._13;

public class PuzzleState
{
    public char[,] Map { get; init; }

    public List<Cart> Carts { get; init; }

    public Point CollisionPoint { get; set; }

    public bool IsFinalState { get; init; }
}