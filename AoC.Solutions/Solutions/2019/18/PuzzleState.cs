using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2019._18;

public class PuzzleState
{
    public char[,] Map { get; init; }
    
    public string Path { get; init; }
    
    public Dictionary<string, List<Point>> Paths { get; init; }
}