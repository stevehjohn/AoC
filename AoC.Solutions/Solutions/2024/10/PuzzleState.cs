namespace AoC.Solutions.Solutions._2024._10;

public class PuzzleState
{
    public char[,] Map { get; private set; }
    
    public List<(int X, int Y)> Visited { get; private set; }

    public PuzzleState(char[,] map, HashSet<(int X, int Y)> visited)
    {
        Map = map;
        
        Visited = visited.ToList();
    }
}