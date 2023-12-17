using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._17;

[UsedImplicitly]
public class Part1 : Base
{
    private int[,] _map;

    private int _width;

    private int _height;
    
    public override string GetAnswer()
    {
        ParseInput();
        
        return Solve().ToString();
    }

    private int Solve()
    {
        var queue = new PriorityQueue<(int X, int Y, char Direction, int Steps, int Cost), int>();

        var visited = new HashSet<(int X, int Y, char Direction)>();
        
        queue.Enqueue((0, 0, 'E', -1, _map[0, 0]), _map[0, 0]);

        while (queue.Count > 0)
        {
            var item = queue.Dequeue();

            if (! visited.Add((item.X, item.Y, item.Direction)))
            {
                continue;
            }

            if (item.X == _width - 1 && item.Y == _height - 1)
            {
                return item.Cost;
            }

            if (item.Direction == 'E')
            {
                if (item.Steps < 3 && item.X < _width - 1)
                {
                    queue.Enqueue((item.X + 1, item.Y, 'E', item.Steps + 1, item.Cost + _map[item.X + 1, item.Y]), _map[item.X + 1, item.Y]);
                }

                if (item.Y > 0)
                {
                    queue.Enqueue((item.X, item.Y - 1, 'N', 0, item.Cost + _map[item.X, item.Y - 1]), _map[item.X, item.Y - 1]);
                }

                if (item.Y < _height - 1)
                {
                    queue.Enqueue((item.X, item.Y + 1, 'S', 0, item.Cost + _map[item.X, item.Y + 1]), _map[item.X, item.Y + 1]);
                }
            }

            if (item.Direction == 'S')
            {
                if (item.Steps < 3 && item.Y < _height - 1)
                {
                    queue.Enqueue((item.X, item.Y + 1, 'S', item.Steps + 1, item.Cost + _map[item.X, item.Y + 1]), _map[item.X, item.Y + 1]);
                }

                if (item.X > 0)
                {
                    queue.Enqueue((item.X - 1, item.Y, 'W', 0, item.Cost + _map[item.X - 1, item.Y]), _map[item.X - 1, item.Y]);
                }

                if (item.X < _width - 1)
                {
                    queue.Enqueue((item.X + 1, item.Y, 'E', 0, item.Cost + _map[item.X + 1, item.Y]), _map[item.X + 1, item.Y]);
                }
            }

            if (item.Direction == 'W')
            {
                if (item.Steps < 3 && item.X > 0)
                {
                    queue.Enqueue((item.X - 1, item.Y, 'W', item.Steps + 1, item.Cost + _map[item.X - 1, item.Y]), _map[item.X - 1, item.Y]);
                }

                if (item.Y > 0)
                {
                    queue.Enqueue((item.X, item.Y - 1, 'N', 0, item.Cost + _map[item.X, item.Y - 1]), _map[item.X, item.Y - 1]);
                }

                if (item.Y < _height - 1)
                {
                    queue.Enqueue((item.X, item.Y + 1, 'S', 0, item.Cost + _map[item.X, item.Y + 1]), _map[item.X, item.Y + 1]);
                }
            }

            if (item.Direction == 'N')
            {
                if (item.Steps < 3 && item.Y > 0)
                {
                    queue.Enqueue((item.X, item.Y - 1, 'N', item.Steps + 1, item.Cost + _map[item.X, item.Y - 1]), _map[item.X, item.Y - 1]);
                }

                if (item.X > 0)
                {
                    queue.Enqueue((item.X - 1, item.Y, 'W', 0, item.Cost + _map[item.X - 1, item.Y]), _map[item.X - 1, item.Y]);
                }

                if (item.X < _width - 1)
                {
                    queue.Enqueue((item.X + 1, item.Y, 'E', 0, item.Cost + _map[item.X + 1, item.Y]), _map[item.X + 1, item.Y]);
                }
            }
        }

        return 0;
    }

    private void ParseInput()
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