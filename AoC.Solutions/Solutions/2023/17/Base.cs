using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2023._17;

public abstract class Base : Solution
{
    public override string Description => "Clumsy crucible";

    private int[,] _map;

    private int _width;

    private int _height;

    protected int Solve(int minSteps, int maxSteps)
    {
        var north = (0, -1);
        var east = (1, 0);
        var south = (0, 1);
        var west = (-1, 0);
        var none = (0, 0);
        
        var queue = new PriorityQueue<(int X, int Y, (int Dx, int Dy) Direction, int Steps), int>();

        var visited = new HashSet<string>();

        queue.Enqueue((0, 0, east, 1), 0);
        queue.Enqueue((0, 0, south, 1), 0);

        var directions = new (int Dx, int Dy)[4];
        
        while (queue.TryDequeue(out var item, out var cost))
        {
            if (item.X == _width - 1 && item.Y == _height - 1 && item.Steps >= minSteps - 1)
            {
                return cost;
            }
            
            if (item.Steps < minSteps - 1)
            {
                directions[0] = item.Direction;
                directions[1] = none;
                directions[2] = none;
                directions[3] = none;
            }
            else
            {
                directions[0] = north;
                directions[1] = east;
                directions[2] = south;
                directions[3] = west;

                switch (item.Direction)
                {
                    case (0, -1):
                        directions[2] = none;
                        break;

                    case (1, 0):
                        directions[3] = none;
                        break;

                    case (0, 1):
                        directions[0] = none;
                        break;

                    case (-1, 0):
                        directions[1] = none;
                        break;
                }
            }

            for (var i = 0; i < 4; i++)
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