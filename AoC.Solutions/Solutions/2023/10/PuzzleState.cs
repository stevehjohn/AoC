using System.Collections.Concurrent;

namespace AoC.Solutions.Solutions._2023._10;

public class PuzzleState
{
    public char[][] Map { get; set; }
    
    public readonly ConcurrentQueue<(int X, int Y, char Change)> Changes = new();
}