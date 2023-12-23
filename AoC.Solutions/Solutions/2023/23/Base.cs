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
        SolveInternal(isPart2, (1, 1, South, 1));

        return _stepCounts.Max();
    }
    
    private void SolveInternal(bool isPart2, (int X, int Y, (int Dx, int Dy) Direction, int Steps) position)
    {
        if (position.X == _width - 2 && position.Y == _height - 1)
        {
            if (isPart2 && ! _stepCounts.Contains(position.Steps))
            {
                if (position.Steps > _max)
                {
                    _max = position.Steps;
                }

                Console.WriteLine($"{DateTime.Now:hh:mm:ss}  Found: {position.Steps} in {_sw.Elapsed}. Current Max: {_max}");

                _sw.Restart();
            }

            _stepCounts.Add(position.Steps);
            
            return;
        }
        
        if (! isPart2)
        {
            var tile = _map[position.X, position.Y];

            if (tile == '>')
            {
                AddNewPosition(true, position, East);

                return;
            }

            if (tile == 'v')
            {
                AddNewPosition(true, position, South);

                return;
            }
        }

        if (position.Direction != South)
        {
            AddNewPosition(isPart2, position, North);
        }

        if (position.Direction != North)
        {
            AddNewPosition(isPart2, position, South);
        }

        if (position.Direction != East)
        {
            AddNewPosition(isPart2, position, West);
        }

        if (position.Direction != West)
        {
            AddNewPosition(isPart2, position, East);
        }
    }

    private void AddNewPosition(
        bool isPart2,
        (int X, int Y, (int Dx, int Dy) Direction, int Steps) position,
        (int Dx, int Dy) newDirection)
    {
        if (_map[position.X + newDirection.Dx, position.Y + newDirection.Dy] != '#')
        {
            if (_visited.Add((position.X + newDirection.Dx, position.Y + newDirection.Dy)))
            {
                SolveInternal(isPart2, (position.X + newDirection.Dx, position.Y + newDirection.Dy, newDirection, position.Steps + 1));

                _visited.Remove((position.X + newDirection.Dx, position.Y + newDirection.Dy));
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