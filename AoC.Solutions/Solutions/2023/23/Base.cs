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

    private readonly HashSet<(int, int)> _visited = new();

    private readonly List<int> _stepCounts = new();

    private int _max;

    private readonly Stopwatch _sw = Stopwatch.StartNew();
    
    protected int Solve(bool isPart2 = false)
    {
        SolveInternal(isPart2, (1, 0, South, 0), 1);

        return _stepCounts.Max();
    }
    
    private void SolveInternal(bool isPart2, (int X, int Y, (int Dx, int Dy) Direction, int Steps) position, int depth)
    {
        start:
        if (position.X == _width - 2 && position.Y == _height - 1)
        {
            if (isPart2 && ! _stepCounts.Contains(position.Steps))
            {
                if (position.Steps > _max)
                {
                    _max = position.Steps;
                }

                Console.WriteLine($"{DateTime.Now:hh:mm:ss}  Found: {position.Steps} in {_sw.Elapsed} Depth: {depth}. Current Max: {_max}");

                _sw.Restart();
            }

            _stepCounts.Add(position.Steps);
            
            return;
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


            if (position.X == _width - 2 && position.Y == _height - 1)
            {
                goto start;
            }
            
            count = 0;
            
            count += _map[position.X - 1, position.Y] == '#' ? 1 : 0;
            count += _map[position.X + 1, position.Y] == '#' ? 1 : 0;
            count += _map[position.X, position.Y - 1] == '#' ? 1 : 0;
            count += _map[position.X, position.Y + 1] == '#' ? 1 : 0;

            if (count != 2)
            {
                _visited.Add((position.X, position.Y));
                
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
                AddNewPosition(true, position, East, depth);

                return;
            }

            if (tile == 'v')
            {
                AddNewPosition(true, position, South, depth);

                return;
            }
        }

        if (position.Direction != South)
        {
            AddNewPosition(isPart2, position, North, depth);
        }

        if (position.Direction != North)
        {
            AddNewPosition(isPart2, position, South, depth);
        }

        if (position.Direction != East)
        {
            AddNewPosition(isPart2, position, West, depth);
        }

        if (position.Direction != West)
        {
            AddNewPosition(isPart2, position, East, depth);
        }
    }

    private void AddNewPosition(
        bool isPart2,
        (int X, int Y, (int Dx, int Dy) Direction, int Steps) position,
        (int Dx, int Dy) newDirection,
        int depth)
    {
        if (_map[position.X + newDirection.Dx, position.Y + newDirection.Dy] != '#')
        {
            if (_visited.Add((position.X + newDirection.Dx, position.Y + newDirection.Dy)))
            {
                // TODO: Pass in isPart2
                SolveInternal(isPart2, (position.X + newDirection.Dx, position.Y + newDirection.Dy, newDirection, position.Steps + 1), depth + 1);
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