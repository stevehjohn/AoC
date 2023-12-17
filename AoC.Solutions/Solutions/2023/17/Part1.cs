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

            //Console.WriteLine($"{item.X}, {item.Y}: {item.Direction} ({item.Cost})");
            
            if (! visited.Add((item.X, item.Y, item.Direction)))
            {
                continue;
            }

            if (item.X == _width - 1 && item.Y == _height - 1)
            {
                return item.Cost;
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
            
            for (var i = 0; i < 4; i++)
            {
                var newSteps = directions[i] == item.Direction ? item.Steps + 1 : 0;
                
                if (directions[i] == 'N' && item.Y > 0)
                {
                    queue.Enqueue((item.X, item.Y - 1, 'N', newSteps, item.Cost + _map[item.X, item.Y - 1]), _map[item.X, item.Y - 1]);
                }

                if (directions[i] == 'E' && item.X < _width - 1)
                {
                    queue.Enqueue((item.X + 1, item.Y, 'E', newSteps, item.Cost + _map[item.X + 1, item.Y]), _map[item.X + 1, item.Y]);
                }

                if (directions[i] == 'S' && item.Y < _height - 1)
                {
                    queue.Enqueue((item.X, item.Y + 1, 'S', newSteps, item.Cost + _map[item.X, item.Y + 1]), _map[item.X, item.Y + 1]);
                }

                if (directions[i] == 'W' && item.X > 0)
                {
                    queue.Enqueue((item.X - 1, item.Y, 'W', newSteps, item.Cost + _map[item.X - 1, item.Y]), _map[item.X - 1, item.Y]);
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