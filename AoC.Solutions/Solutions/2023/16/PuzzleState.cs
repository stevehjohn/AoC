namespace AoC.Solutions.Solutions._2023._16;

public class PuzzleState
{
    public char[,] Map { get; set; }

    public List<(int X, int Y, char Direction, int Id, int SourceId)> Beams { get; set; } = new();

    public char StartDirection { get; set; }

    public int LaserX { get; set; }
    
    public int LaserY { get; set; }
}