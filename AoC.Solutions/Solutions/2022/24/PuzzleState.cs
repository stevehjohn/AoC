namespace AoC.Solutions.Solutions._2022._24;

public class PuzzleState
{
    public char[,] Map { get; set; }

    public List<(int X, int Y)> Moves { get; set; }

    public (int X, int Y) Start { get; set; }
    
    public (int X, int Y) End { get; set; }
}