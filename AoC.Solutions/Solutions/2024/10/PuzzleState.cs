using AoC.Solutions.Extensions;

namespace AoC.Solutions.Solutions._2024._10;

public class PuzzleState
{
    public static char[,] Map { get; private set; }
    
    public List<(int X, int Y)> Visited { get; private set; }

    public static List<(int X, int Y)> AllVisited { get; private set; } = [];

    public PuzzleState(char[,] map, HashSet<(int X, int Y)> visited)
    {
        if (Map == null)
        {
            Map = new char[map.GetLength(0) * 2, map.GetLength(1) * 2];

            map.ForAll((x, y, c) =>
            {
                Map[x * 2, y * 2] = c;
                Map[x * 2 + 1, y * 2] = c;
                Map[x * 2, y * 2 + 1] = c;
                Map[x * 2 + 1, y * 2 + 1] = c;
            });
        }
        
        Visited = visited.ToList();
        
        AllVisited.AddRange(Visited);
    }
}