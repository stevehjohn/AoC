using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2022._24;

public abstract class Base : Solution
{
    public override string Description => "Blizzard basin";

    private readonly List<(Point Position, char Direction)> _storms = new();

    private int _width;

    private int _height;

    private Point _start;

    private Point _end;

    protected void ParseInput()
    {
        _width = Input[0].Length;

        _height = Input.Length;

        for (var y = 0; y < _height; y++)
        {
            var line = Input[y];

            for (var x = 0; x < _width; x++)
            {
                var c = line[x];

                if (y == 0 && c == '.')
                {
                    _start = new Point(x, y);
                }

                if (y == Input.Length - 1 && c == '.')
                {
                    _end = new Point(x, y);
                }

                if (c == '#' || c == '.')
                {
                    continue;
                }

                _storms.Add((new Point(x, y), c));
            }
        }
    }

    protected void RunSimulation()
    {
        while (true)
        {
            MoveStorms();
        }
    }

    private void MoveStorms()
    {
        foreach (var storm in _storms)
        {
            switch (storm.Direction)
            {
                case '^':
                    storm.Position.Y--;

                    if (storm.Position.Y == 0)
                    {
                        storm.Position.Y = _height - 2;
                    }

                    break;

                case '>':
                    storm.Position.X++;

                    if (storm.Position.X == _width - 1)
                    {
                        storm.Position.X = 1;
                    }

                    break;

                case 'v':
                    storm.Position.Y++;

                    if (storm.Position.Y == _height - 1)
                    {
                        storm.Position.Y = 1;
                    }

                    break;

                case '<':
                    storm.Position.X--;

                    if (storm.Position.X == 0)
                    {
                        storm.Position.X = _width - 2;
                    }

                    break;
            }
        }
    }
}