using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._21;

[UsedImplicitly]
public class Part1 : Base
{
    private char[,] _map;

    private int _width;

    private int _height;

    public override string GetAnswer()
    {
        var start = ParseInput();

        var plots = Walk(start, 64);
        
        return plots.ToString();
    }

    private int Walk((int X, int Y) start, int maxSteps)
    {
        var positions = new List<(int X, int Y)>
        {
            (start.X, start.Y)
        };

        for (var i = 0; i < maxSteps; i++)
        {
            var newPositions = new List<(int X, int Y)>();

            foreach (var position in positions)
            {
                if (position.X > 0 && _map[position.X - 1, position.Y] == '.' && ! newPositions.Contains((position.X - 1, position.Y)))
                {
                    newPositions.Add((position.X - 1, position.Y));
                }

                if (position.X < _width - 1 && _map[position.X + 1, position.Y] == '.' && ! newPositions.Contains((position.X + 1, position.Y)))
                {
                    newPositions.Add((position.X + 1, position.Y));
                }

                if (position.Y > 0 && _map[position.X, position.Y - 1] == '.' && ! newPositions.Contains((position.X, position.Y - 1)))
                {
                    newPositions.Add((position.X, position.Y - 1));
                }

                if (position.Y < _height - 1 && _map[position.X, position.Y + 1] == '.' && ! newPositions.Contains((position.X, position.Y + 1)))
                {
                    newPositions.Add((position.X, position.Y + 1));
                }
            }

            positions = newPositions;
        }

        return positions.Count;
    }

    private (int X, int Y) ParseInput()
    {
        _width = Input[0].Length;

        _height = Input.Length;
        
        _map = new char[_width, _height];

        var y = 0;

        (int X, int Y) start = (0, 0);
        
        foreach (var line in Input)
        {
            for (var x = 0; x < line.Length; x++)
            {
                _map[x, y] = line[x];

                if (line[x] == 'S')
                {
                    start = (x, y);
                    _map[x, y] = '.';
                }
            }

            y++;
        }

        return start;
    }
}