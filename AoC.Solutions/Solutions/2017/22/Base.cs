using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2017._22;

public abstract class Base : Solution
{
    public override string Description => "Sporifica virus";

    private readonly List<Point> _infected = new();

    private Point _position;

    private Point _direction = new(0, -1);

    protected bool RunCycle()
    {
        var infected = _infected.SingleOrDefault(i => i.Equals(_position));

        var infects = false;

        if (infected != null)
        {
            _direction = new Point(-_direction.Y, _direction.X);

            _infected.Remove(infected);
        }
        else
        {
            _direction = new Point(_direction.Y, -_direction.X);

            _infected.Add(new Point(_position));

            infects = true;
        }

        _position.X += _direction.X;

        _position.Y += _direction.Y;

        return infects;
    }

    protected void ParseInput()
    {
        var y = 0;

        foreach (var line in Input)
        {
            for (var x = 0; x < line.Length; x++)
            {
                if (line[x] == '#')
                {
                    _infected.Add(new Point(x, y));
                }
            }

            y++;
        }

        _position = new Point(Input[0].Length / 2, Input.Length / 2);
    }
}