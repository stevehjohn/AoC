using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2023._17;

public abstract class Base : Solution
{
    public override string Description => "Clumsy crucible";

    private int[,] _map;

    private int _width;

    private int _height;

    private static readonly (int, int) North = (0, -1);
    
    private static readonly (int, int) East = (1, 0);
    
    private static readonly (int, int) South = (0, 1);
    
    private static readonly (int, int) West = (-1, 0);
    
    private static readonly (int, int) None = (0, 0);

    protected int Solve(int minSteps, int maxSteps)
    {
        var queue = new PriorityQueue<(int X, int Y, (int Dx, int Dy) Direction, int Steps), int>();

        var visited = new HashSet<string>();

        queue.Enqueue((0, 0, East, 1), 0);
        queue.Enqueue((0, 0, South, 1), 0);

        var directions = new (int Dx, int Dy)[4];
        
        while (queue.TryDequeue(out var item, out var cost))
        {
            if (item.X == _width - 1 && item.Y == _height - 1 && item.Steps >= minSteps - 1)
            {
                return cost;
            }

            int directionCount;
            
            if (item.Steps < minSteps - 1)
            {
                directions[0] = item.Direction;

                directionCount = 1;
            }
            else
            {
                GetDirections(directions, item.Direction);

                directionCount = 4;
            }

            for (var i = 0; i < directionCount; i++)
            {
                var direction = directions[i];
                
                if (direction == (0, 0))
                {
                    continue;
                }

                var newSteps = directions[i] == item.Direction ? item.Steps + 1 : 0;

                if (newSteps == maxSteps)
                {
                    continue;
                }

                var (x, y) = (item.X + direction.Dx, item.Y + direction.Dy);

                if (x < 0 || x == _width || y < 0 || y == _height)
                {
                    continue;
                }
                
                var key = $"{item.X},{item.Y},{x},{y},{newSteps}";
                
                if (visited.Add(key))
                {
                    queue.Enqueue((x, y, direction, newSteps), cost + _map[x, y]);
                }
            }
        }

        return 0;
    }

    private static void GetDirections((int, int)[] directions, (int, int) direction)
    {
        directions[0] = North;
        directions[1] = East;
        directions[2] = South;
        directions[3] = West;

        switch (direction)
        {
            case (0, -1):
                directions[2] = None;
                break;

            case (1, 0):
                directions[3] = None;
                break;

            case (0, 1):
                directions[0] = None;
                break;

            case (-1, 0):
                directions[1] = None;
                break;
        }
    }

    protected void ParseInput()
    {
        _width = Input[0].Length;

        _height = Input.Length;

        _map = new int[_width, _height];

        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                _map[x, y] = Input[y][x] - '0';
            }
        }
    }
}