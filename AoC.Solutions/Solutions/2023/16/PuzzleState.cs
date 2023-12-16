namespace AoC.Solutions.Solutions._2023._16;

public class PuzzleState
{
    public char[,] Map { get; set; }
    
    public (int X, int Y, char Direction, int Id)? NewBeam { get; set; }
}