namespace AoC.Solutions.Solutions._2024._10;

public class PuzzleState
{
    public static char[,] Map { get; private set; }
    
    public List<(int X, int Y)> Visited { get; private set; }

    public static List<(int X, int Y)> AllVisited { get; } = [];

    public PuzzleState(char[,] map, HashSet<(int X, int Y)> visited)
    {
        Map ??= map;
        
        Visited = visited.ToList();

        foreach (var item in visited)
        {
            if (! AllVisited.Contains(item))
            {
                AllVisited.Add(item);
            }
        }
    }
}