using AoC.Solutions.Extensions;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._23;

[UsedImplicitly]
public class Part2 : Base
{
    private char[,] _map;

    private int _width;

    private int _height;

    private static readonly (int, int) North = (0, -1);

    private static readonly (int, int) East = (1, 0);

    private static readonly (int, int) South = (0, 1);

    private static readonly (int, int) West = (-1, 0);

    private readonly HashSet<(int, int)> _visited = new();

    private readonly List<int> _stepCounts = new();
    
    public override string GetAnswer()
    {
        ParseInput();

        var result = Solve();

        return result.ToString();
    }
    
    private int Solve()
    {
        SolveInternal((1, 1, South, 1));

        return _stepCounts.Max();
    }

    private void SolveInternal((int X, int Y, (int Dx, int Dy) Direction, int Steps) position)
    {
        if (position.X == _width - 2 && position.Y == _height - 1)
        {
            _stepCounts.Add(position.Steps);

            return;
        }

        if (position.Direction != South)
        {
            AddNewPosition(position, North);
        }

        if (position.Direction != North)
        {
            AddNewPosition(position, South);
        }

        if (position.Direction != East)
        {
            AddNewPosition(position, West);
        }

        if (position.Direction != West)
        {
            AddNewPosition(position, East);
        }
    }

    private void AddNewPosition(
        (int X, int Y, (int Dx, int Dy) Direction, int Steps) position,
        (int Dx, int Dy) newDirection)
    {
        if (_map[position.X + newDirection.Dx, position.Y + newDirection.Dy] != '#')
        {
            if (_visited.Add((position.X + newDirection.Dx, position.Y + newDirection.Dy)))
            {
                SolveInternal((position.X + newDirection.Dx, position.Y + newDirection.Dy, newDirection, position.Steps + 1));

                _visited.Remove((position.X + newDirection.Dx, position.Y + newDirection.Dy));
            }
        }
    }

    private void ParseInput()
    {
        _map = Input.To2DArray();

        _width = _map.GetLength(0);

        _height = _map.GetLength(1);
    }
}