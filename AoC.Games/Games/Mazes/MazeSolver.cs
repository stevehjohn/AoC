using AoC.Solutions.Extensions;

namespace AoC.Games.Games.Mazes;

public class MazeSolver
{
    private readonly bool[,] _maze;

    private (int X, int Y) _entrance = (-1, -1);

    private (int X, int Y) _exit = (-1, -1);

    public MazeSolver(bool[,] maze)
    {
        _maze = maze;
    }

    public (List<(int X, int Y)> Path, List<(int X, int Y)> Visited) SolveMaze()
    {
        FindEntranceAndExit();

        var queue = new Queue<(int X, int Y, List<(int X, int Y)> History)>();

        queue.Enqueue((_entrance.X, _entrance.Y, []));

        var visited = new List<(int X, int Y)>();

        while (queue.TryDequeue(out var node))
        {
            node.History.Add((node.X, node.Y));

            if (! visited.Contains((node.X, node.Y)))
            {
                visited.Add((node.X, node.Y));
            }

            if (node.X == _exit.X && node.Y == _exit.Y)
            {
                return (node.History, visited);
            }

            var moves = GetMoves(node.X, node.Y);

            moves.ForAll((_, m) =>
            {
                if (! node.History.Contains(m))
                {
                    queue.Enqueue((m.X, m.Y, [..node.History]));
                }
            });
        }

        return ([], visited);
    }

    private void FindEntranceAndExit()
    {
        for (var x = 0; x < Constants.Width; x++)
        {
            (int X, int Y) position = (-1, -1);

            if (_maze[x, 0])
            {
                position = (x, 0);
            }
            else if (_maze[x, Constants.Height - 1])
            {
                position = (x, Constants.Height - 1);
            }

            if (position.X == -1)
            {
                continue;
            }

            if (_entrance.X == -1)
            {
                _entrance = position;
            }
            else
            {
                _exit = position;
            }
        }

        for (var y = 0; y < Constants.Height; y++)
        {
            (int X, int Y) position = (-1, -1);

            if (_maze[0, y])
            {
                position = (0, y);
            }
            else if (_maze[Constants.Width - 1, y])
            {
                position = (Constants.Width - 1, y);
            }

            if (position.X == -1)
            {
                continue;
            }

            if (_entrance.X == -1)
            {
                _entrance = position;
            }
            else
            {
                _exit = position;
            }
        }
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