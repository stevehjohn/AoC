using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2019._18;

public class PuzzleState
{
    public char[,] Map { get; set; }
    
    public string Path { get; set; }
    
    public Dictionary<string, List<Point>> Paths { get; set; }
}