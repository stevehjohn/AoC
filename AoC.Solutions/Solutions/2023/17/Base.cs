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
        var queue = new PriorityQueue<(int X, int Y, char Direction, int Steps), int>();

        var visited = new HashSet<string>();

        queue.Enqueue((0, 0, 'E', 1), 0);
        queue.Enqueue((0, 0, 'S', 1), 0);

        while (queue.TryDequeue(out var item, out var cost))
        {
            if (item.X == _width - 1 && item.Y == _height - 1 && item.Steps >= minSteps - 1)
            {
                return cost;
            }

            var directions = new[] { 'N', 'E' ,'S', 'W' };

            switch (item.Direction)
            {
                case 'N':
                    directions[2] = ' ';
                    break;

                case 'E':
                    directions[3] = ' ';
                    break;

                case 'S':
                    directions[0] = ' ';
                    break;

                case 'W':
                    directions[1] = ' ';
                    break;
            }
            
            if (item.Steps < minSteps - 1)
            {
                directions[0] = item.Direction;
                directions[1] = ' ';
                directions[2] = ' ';
                directions[3] = ' ';
            }
            
            for (var i = 0; i < 4; i++)
            {
                if (directions[i] == ' ')
                {
                    continue;
                }

                var newSteps = directions[i] == item.Direction ? item.Steps + 1 : 0;

                if (newSteps == maxSteps)
                {
                    continue;
                }

                if (directions[i] == 'E' && item.X < _width - 1)
                {
                    var key = $"{item.X},{item.Y},{item.X + 1},{item.Y},{newSteps}";

                    if (visited.Add(key))
                    {
                        queue.Enqueue((item.X + 1, item.Y, 'E', newSteps), cost + _map[item.X + 1, item.Y]);
                    }
                }

                if (directions[i] == 'S' && item.Y < _height - 1)
                {
                    var key = $"{item.X},{item.Y},{item.X},{item.Y + 1},{newSteps}";

                    if (visited.Add(key))
                    {
                        queue.Enqueue((item.X, item.Y + 1, 'S', newSteps), cost + _map[item.X, item.Y + 1]);
                    }
                }

                if (directions[i] == 'N' && item.Y > 0)
                {
                    var key = $"{item.X},{item.Y},{item.X},{item.Y - 1},{newSteps}";

                    if (visited.Add(key))
                    {
                        queue.Enqueue((item.X, item.Y - 1, 'N', newSteps), cost + _map[item.X, item.Y - 1]);
                    }
                }

                if (directions[i] == 'W' && item.X > 0)
                {
                    var key = $"{item.X},{item.Y},{item.X - 1},{item.Y},{newSteps}";

                    if (visited.Add(key))
                    {
                        queue.Enqueue((item.X - 1, item.Y, 'W', newSteps), cost + _map[item.X - 1, item.Y]);
                    }
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