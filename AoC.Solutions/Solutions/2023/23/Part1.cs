using AoC.Solutions.Extensions;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._23;

[UsedImplicitly]
public class Part1 : Base
{
    private char[,] _map;

    private int _width;

    private int _height;

    private int _pathTiles;
    
    private static readonly (int, int) North = (0, -1);
    
    private static readonly (int, int) East = (1, 0);
    
    private static readonly (int, int) South = (0, 1);
    
    private static readonly (int, int) West = (-1, 0);
    
    public override string GetAnswer()
    {
        ParseInput();

        var result = Solve();
        
        return result.ToString();
    }
    
    private int Solve(bool isPart2 = false)
    {
        var queue = new Queue<(int X, int Y, (int Dx, int Dy) Direction, int Steps, HashSet<(int X, int Y)> Visited)>();
        
        queue.Enqueue((1, 0, South, 0, new HashSet<(int X, int Y)>()));

        var stepCounts = new List<int>();
        
        while (queue.TryDequeue(out var position))
        {
            if (position.Steps > _pathTiles)
            {
                continue;
            }

            if (position.X == _width - 2 && position.Y == _height - 1)
            {
                stepCounts.Add(position.Steps);

                if (isPart2)
                {
                    Console.WriteLine(position.Steps);
                }

                continue;
            }

            var tile = _map[position.X, position.Y];

            if (! isPart2)
            {
                if (tile == '>')
                {
                    AddNewPosition(queue, position, East);

                    continue;
                }

                if (tile == 'v')
                {
                    AddNewPosition(queue, position, South);

                    continue;
                }
            }

            if (position.Direction != North)
            {
                AddNewPosition(queue, position, South);
            }

            if (position.Direction != East)
            {
                AddNewPosition(queue, position, West);
            }

            if (position.Direction != South)
            {
                AddNewPosition(queue, position, North);
            }

            if (position.Direction != West)
            {
                AddNewPosition(queue, position, East);
            }
        }
        
        return stepCounts.Max();
    }

    private void AddNewPosition(
        Queue<(int X, int Y, (int Dx, int Dy) Direction, int Steps, HashSet<(int X, int Y)>)> queue,
        (int X, int Y, (int Dx, int Dy) Direction, int Steps, HashSet<(int X, int Y)> Visited) position, 
        (int Dx, int Dy) direction)
    {
        if (_map[position.X + direction.Dx, position.Y + direction.Dy] != '#')
        {
            if (position.Visited.Add((position.X + direction.Dx, position.Y + direction.Dy)))
            {
                queue.Enqueue((position.X + direction.Dx, position.Y + direction.Dy, direction, position.Steps + 1,
                    new HashSet<(int X, int Y)>(position.Visited)));
            }
        }
    }

    private void ParseInput()
    {
        _map = Input.To2DArray();

        _width = _map.GetLength(0);

        _height = _map.GetLength(1);

        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                if (_map[x, y] != '#')
                {
                    _pathTiles++;
                }
            }
        }
    }
}