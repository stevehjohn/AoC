using System.Diagnostics;
using AoC.Solutions.Extensions;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2023._23;

public abstract class Base : Solution
{
    public override string Description => "A long walk";
    
    private char[,] _map;

    private int _width;

    private int _height;

    private static readonly (int, int) North = (0, -1);
    
    private static readonly (int, int) East = (1, 0);
    
    private static readonly (int, int) South = (0, 1);
    
    private static readonly (int, int) West = (-1, 0);
    
    protected int Solve(bool isPart2 = false)
    {
        var queue = new Queue<(int X, int Y, (int Dx, int Dy) Direction, (int Dx, int Dy) SourceDirection, int Steps, HashSet<(int X, int Y)> Visited)>();
        
        queue.Enqueue((1, 1, South, South, 1, new HashSet<(int X, int Y)>()));

        var stepCounts = new List<int>();

        var sw = Stopwatch.StartNew();

        var maxSteps = new Dictionary<(int, int, int, int, int, int), int>();

        var ignored = 0;

        var max = 0;
        
        while (queue.TryDequeue(out var position))
        {
            // var key = (position.X, position.Y, position.Direction.Dx, position.Direction.Dy, position.SourceDirection.Dx, position.SourceDirection.Dy);
            //
            // if (maxSteps.ContainsKey(key))
            // {
            //     if (position.Steps > maxSteps[key])
            //     {
            //         maxSteps[key] = position.Steps;
            //     }
            //     else
            //     {
            //         ignored++;
            //         
            //         continue;
            //     }
            // }
            // else
            // {
            //     maxSteps[key] = position.Steps;
            // }

            if (position.X == _width - 2 && position.Y == _height - 1)
            {
                stepCounts.Add(position.Steps);

                if (position.Steps > max)
                {
                    max = position.Steps;

                    if (isPart2)
                    {
                        Console.WriteLine($"{position.Steps}: {sw.Elapsed} ({queue.Count}). Ignored {ignored}. Max: {max}.");
        
                        sw.Restart();
                    }
                }
                
                continue;
            }
            
            var count = 2;
            
            while (count == 2 && _map[position.X + position.Direction.Dx, position.Y + position.Direction.Dy] == '.')
            {
                count = 0;
                
                count += _map[position.X - 1, position.Y] == '#' ? 1 : 0;
                count += _map[position.X + 1, position.Y] == '#' ? 1 : 0;
                count += _map[position.X, position.Y - 1] == '#' ? 1 : 0;
                count += _map[position.X, position.Y + 1] == '#' ? 1 : 0;
                
                position.X += position.Direction.Dx;
                position.Y += position.Direction.Dy;
            
                position.Steps++;
            
                position.Visited.Add((position.X, position.Y));
            }
            
            if (! isPart2)
            {
                var tile = _map[position.X, position.Y];

                if (tile == '>')
                {
                    AddNewPosition(queue, position, position.Direction, East);

                    continue;
                }

                if (tile == 'v')
                {
                    AddNewPosition(queue, position, position.Direction, South);

                    continue;
                }
            }

            if (position.Direction != South)
            {
                AddNewPosition(queue, position, position.Direction, North);
            }

            if (position.Direction != North)
            {
                AddNewPosition(queue, position, position.Direction, South);
            }

            if (position.Direction != East)
            {
                AddNewPosition(queue, position, position.Direction, West);
            }

            if (position.Direction != West)
            {
                AddNewPosition(queue, position, position.Direction, East);
            }
        }

        return stepCounts.Max();
    }

    private void AddNewPosition(
        Queue<(int X, int Y, (int Dx, int Dy) Direction, (int Dx, int Dy) SourceDirection, int Steps, HashSet<(int X, int Y)>)> queue,
        (int X, int Y, (int Dx, int Dy) Direction, (int Dx, int Dy) SourceDirection, int Steps, HashSet<(int X, int Y)> Visited) position, 
        (int Dx, int Dy) direction,
        (int Dx, int Dy) newDirection)
    {
        if (_map[position.X + newDirection.Dx, position.Y + newDirection.Dy] != '#')
        {
            if (position.Visited.Add((position.X + newDirection.Dx, position.Y + newDirection.Dy)))
            {
                if (direction != newDirection)
                {
                    queue.Enqueue((position.X + newDirection.Dx, position.Y + newDirection.Dy, newDirection, position.Direction, position.Steps + 1,
                        new HashSet<(int X, int Y)>(position.Visited)));
                }
                else
                {
                    queue.Enqueue((position.X + newDirection.Dx, position.Y + newDirection.Dy, newDirection, position.Direction, position.Steps + 1,
                        position.Visited));
                }
            }
        }
    }

    protected void ParseInput()
    {
        _map = Input.To2DArray();

        _width = _map.GetLength(0);

        _height = _map.GetLength(1);
    }
}