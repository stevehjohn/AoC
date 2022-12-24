using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2022._24;

public abstract class Base : Solution
{
    public override string Description => "Blizzard basin";

    private Storm[] _storms;

    private int _stormCount;

    private int _width;

    private int _height;

    private Point _start;

    private Point _end;

    protected void ParseInput()
    {
        _width = Input[0].Length;

        _height = Input.Length;

        for (var y = 1; y < _height - 1; y++)
        {
            var line = Input[y];

            for (var x = 1; x < _width - 1; x++)
            {
                if (line[x] != '.')
                {
                    _stormCount++;
                }
            }
        }

        _storms = new Storm[_stormCount];

        var i = 0;

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

                _storms[i] = new Storm(c, x, y);
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

    private Storm[] MoveStorms()
    {
        var storms = new Storm[_stormCount];

        for (var i = 0; i < _stormCount; i++)
        {
            var storm = _storms[i];

            int x;

            int y;

            switch (storm.Direction)
            {
                case '^': 
                    y = storm.Y - 1;

                    if (y == 0)
                    {
                        y = _height - 2;
                    }

                    storms[i] = new Storm(storm.Direction, storm.X, y);

                    continue;

                case '>':
                    x = storm.X + 1;

                    if (x == _width - 1)
                    {
                        x = 1;
                    }

                    storms[i] = new Storm(storm.Direction, x, storm.Y);

                    continue;

                case 'v':
                    y = storm.Y + 1;

                    if (y == _height - 1)
                    {
                        y = 1;
                    }

                    storms[i] = new Storm(storm.Direction, storm.X, y);

                    continue;

                case '<':
                    x = storm.X - 1;

                    if (x == 0)
                    {
                        x = _width - 2;
                    }

                    storms[i] = new Storm(storm.Direction, x, storm.Y);

                    continue;
            }
        }

        return storms;
    }
}