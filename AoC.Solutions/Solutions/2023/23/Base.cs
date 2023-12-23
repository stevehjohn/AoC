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
        var queue = new Queue<(int X, int Y, (int Dx, int Dy) Direction, int Steps, HashSet<(int X, int Y)> Visited)>();
        
        queue.Enqueue((1, 1, South, 1, new HashSet<(int X, int Y)>()));

        var stepCounts = new List<int>();

        var sw = Stopwatch.StartNew();

        var max = 0;
        
        while (queue.TryDequeue(out var position))
        {
            if (position.X == _width - 2 && position.Y == _height - 1)
            {
                stepCounts.Add(position.Steps);

                if (position.Steps > max)
                {
                    max = position.Steps;

                    if (isPart2)
                    {
                        Console.WriteLine($"{DateTime.Now:hh:mm:ss}    {position.Steps}: {sw.Elapsed} ({queue.Count}).");
                    }
        
                    sw.Restart();
                }
                
                continue;
            }
            
            var count = 0;

            count += _map[position.X - 1, position.Y] == '#' ? 1 : 0;
            count += _map[position.X + 1, position.Y] == '#' ? 1 : 0;
            count += _map[position.X, position.Y - 1] == '#' ? 1 : 0;
            count += _map[position.X, position.Y + 1] == '#' ? 1 : 0;
            
            while (count == 2 && _map[position.X + position.Direction.Dx, position.Y + position.Direction.Dy] == '.')
            {
                position.X += position.Direction.Dx;
                position.Y += position.Direction.Dy;
            
                position.Steps++;

                if (position.X == _width - 2 && position.Y == _height - 2)
                {
                    break;
                }
                
                //position.Visited.Add((position.X, position.Y));

                count = 0;
                
                count += _map[position.X - 1, position.Y] == '#' ? 1 : 0;
                count += _map[position.X + 1, position.Y] == '#' ? 1 : 0;
                count += _map[position.X, position.Y - 1] == '#' ? 1 : 0;
                count += _map[position.X, position.Y + 1] == '#' ? 1 : 0;

                if (count != 2)
                {
                    break;
                }

                if (_map[position.X + position.Direction.Dx, position.Y + position.Direction.Dy] == '#')
                {
                    if (position.Direction == North || position.Direction == South)
                    {
                        if (_map[position.X + 1, position.Y] == '.')
                        {
                            position.Direction = East;
                        }
                        else if (_map[position.X - 1, position.Y] == '.')
                        {
                            position.Direction = West;
                        }
                    }
                    else if (position.Direction == East || position.Direction == West)
                    {
                        if (_map[position.X, position.Y + 1] == '.')
                        {
                            position.Direction = South;
                        }
                        else if (_map[position.X, position.Y - 1] == '.')
                        {
                            position.Direction = North;
                        }
                    }
                }
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
        Queue<(int X, int Y, (int Dx, int Dy) Direction, int Steps, HashSet<(int X, int Y)>)> queue,
        (int X, int Y, (int Dx, int Dy) Direction, int Steps, HashSet<(int X, int Y)> Visited) position, 
        (int Dx, int Dy) direction,
        (int Dx, int Dy) newDirection)
    {
        if (_map[position.X + newDirection.Dx, position.Y + newDirection.Dy] != '#')
        {
            if (position.Visited.Add((position.X + newDirection.Dx, position.Y + newDirection.Dy)))
            {
                if (direction != newDirection)
                {
                    queue.Enqueue((position.X + newDirection.Dx, position.Y + newDirection.Dy, newDirection, position.Steps + 1,
                        new HashSet<(int X, int Y)>(position.Visited)));
                }
                else
                {
                    queue.Enqueue((position.X + newDirection.Dx, position.Y + newDirection.Dy, newDirection, position.Steps + 1,
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