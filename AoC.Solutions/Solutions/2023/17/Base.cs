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
        var queue = new PriorityQueue<(int X, int Y, char Direction, int Steps, int Cost), int>();

        var visited = new HashSet<string>();

        queue.Enqueue((0, 0, 'E', 1, 0), _map[0, 0]);
        queue.Enqueue((0, 0, 'S', 1, 0), _map[0, 0]);

        while (queue.Count > 0)
        {
            var item = queue.Dequeue();

            if (item.X == _width - 1 && item.Y == _height - 1 && item.Steps >= minSteps - 1)
            {
                return item.Cost;
            }

            var directions = new[] { 'N', 'E' ,'S', 'W' };

            if (item.Steps < minSteps - 1)
            {
                switch (item.Direction)
                {
                    case 'N':
                        if (item.Y - (minSteps - item.Steps) < 0)
                        {
                            continue;
                        }

                        directions[1] = ' ';
                        directions[2] = ' ';
                        directions[3] = ' ';
                        break;

                    case 'E':
                        if (item.X + (minSteps - item.Steps) > _width)
                        {
                            continue;
                        }
                        
                        directions[0] = ' ';
                        directions[2] = ' ';
                        directions[3] = ' ';
                        break;

                    case 'S':
                        if (item.Y + (minSteps - item.Steps) > _height)
                        {
                            continue;
                        }
                        
                        directions[0] = ' ';
                        directions[1] = ' ';
                        directions[3] = ' ';
                        break;

                    case 'W':
                        if (item.X + (minSteps - item.Steps) < 0)
                        {
                            continue;
                        }
                        
                        directions[0] = ' ';
                        directions[1] = ' ';
                        directions[2] = ' ';
                        break;
                }
            }

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
            
            for (var i = 0; i < 4; i++)
            {
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
                        visited.Add(key);

                        queue.Enqueue((item.X + 1, item.Y, 'E', newSteps, item.Cost + _map[item.X + 1, item.Y]), item.Cost + _map[item.X + 1, item.Y]);
                    }
                }

                if (directions[i] == 'S' && item.Y < _height - 1)
                {
                    var key = $"{item.X},{item.Y},{item.X},{item.Y + 1},{newSteps}";

                    if (visited.Add(key))
                    {
                        visited.Add(key);

                        queue.Enqueue((item.X, item.Y + 1, 'S', newSteps, item.Cost + _map[item.X, item.Y + 1]), item.Cost +  _map[item.X, item.Y + 1]);
                    }
                }

                if (directions[i] == 'N' && item.Y > 0)
                {
                    var key = $"{item.X},{item.Y},{item.X},{item.Y - 1},{newSteps}";

                    if (visited.Add(key))
                    {
                        visited.Add(key);
                    
                        queue.Enqueue((item.X, item.Y - 1, 'N', newSteps, item.Cost + _map[item.X, item.Y - 1]), item.Cost +  _map[item.X, item.Y - 1]);
                    }
                }

                if (directions[i] == 'W' && item.X > 0)
                {
                    var key = $"{item.X},{item.Y},{item.X - 1},{item.Y},{newSteps}";

                    if (visited.Add(key))
                    {
                        visited.Add(key);

                        queue.Enqueue((item.X - 1, item.Y, 'W', newSteps, item.Cost + _map[item.X - 1, item.Y]), item.Cost +  _map[item.X - 1, item.Y]);
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