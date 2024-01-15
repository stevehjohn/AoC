using AoC.Solutions.Extensions;

namespace AoC.Games.Games.Mazes;

public class MazeSolver
{
    private readonly bool[,] _maze;

    public MazeSolver(bool[,] maze)
    {
        _maze = maze;
    }

    public List<(int X, int Y)> SolveMaze()
    {
        var queue = new Queue<(int X, int Y, List<(int X, int Y)> History)>();
        
        queue.Enqueue((1, 0, new List<(int X, int Y)>()));
        
        while (queue.TryDequeue(out var node))
        {
            node.History.Add((node.X, node.Y));
            
            if (node.X == Constants.Width - 2 && node.Y == Constants.Height - 1)
            {
                return node.History;
            }

            var moves = GetMoves(node.X, node.Y);
            
            moves.ForAll((_, m) =>
            {
                if (! node.History.Contains(m))
                {
                    queue.Enqueue((m.X, m.Y, new List<(int, int)>(node.History)));
                }
            });
        }

        return [];
    }

    private List<(int X, int Y)> GetMoves(int x, int y)
    {
        var moves = new List<(int, int)>();

        if (x > 1 && _maze[x - 1, y])
        {
            moves.Add((x - 1, y));
        }

        if (x < Constants.Width - 2 && _maze[x + 1, y])
        {
            moves.Add((x + 1, y));
        }

        if (y > 1 && _maze[x, y - 1])
        {
            moves.Add((x, y - 1));
        }

        if (y < Constants.Height - 1 && _maze[x, y + 1])
        {
            moves.Add((x, y + 1));
        }

        return moves;
    }
}