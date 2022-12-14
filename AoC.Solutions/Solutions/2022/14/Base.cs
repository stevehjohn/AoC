using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2022._14;

public abstract class Base : Solution
{
    public override string Description => "Regolith reservoir";

    private const int Width = 1000;

    private const int Height = 500;

    private char[,] _map;

    private int _maxY;

    private bool _hasFloor;
        
    private readonly IVisualiser<PuzzleState> _visualiser;

//    private Point _position;

    protected Base()
    {
    }

    protected Base(IVisualiser<PuzzleState> visualiser)
    {
        _visualiser = visualiser;
    }

    protected void Visualise()
    {
        if (_visualiser != null)
        {
            _visualiser.PuzzleStateChanged(new PuzzleState(_map, null));
        }
    }
    
    protected void EndVisualisation()
    {
        if (_visualiser != null)
        {
            _visualiser.PuzzleComplete();
        }
    }

    protected void CreateCave()
    {
        _map = new char[Width, Height];

        foreach (var line in Input)
        {
            var points = line.Split("->", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(Point.Parse).ToList();

            var position = points[0];

            _map[position.X, position.Y] = '#';

            for (var i = 1; i < points.Count; i++)
            {
                var target = points[i];

                while (! position.Equals(target))
                {
                    var dX = target.X - position.X;

                    if (dX != 0)
                    {
                        position.X += (dX & 0b10000000_00000000_00000000_00000000) == 0b10000000_00000000_00000000_00000000 ? -1 : 1;
                    }

                    var dY = target.Y - position.Y;

                    if (dY != 0)
                    {
                        position.Y += (dY & 0b10000000_00000000_00000000_00000000) == 0b10000000_00000000_00000000_00000000 ? -1 : 1;
                    }

                    _map[position.X, position.Y] = '#';

                    if (position.Y > _maxY)
                    {
                        _maxY = position.Y;
                    }
                }
            }
        }
    }

    protected int SimulateSand()
    {
        var units = _hasFloor ? 1 : 0;

        var positions = new List<Point> { new(500, 0) };

        var counter = 21;

        var toRemove = new List<Point>();

        while (true)
        {
            if (_visualiser != null)
            {
                counter--;

                if (counter == 0)
                {
                    positions.Add(new Point(500, 0));

                    counter = 20;
                }
            }

            for (var i = 0; i < positions.Count; i++)
            {
                var position = positions[i];

                if (position.Y == _maxY && !_hasFloor)
                {
                    return units;
                }

                if (_map[position.X, position.Y + 1] == '\0')
                {
                    position.Y++;

                    Visualise();

                    continue;
                }

                if (_map[position.X - 1, position.Y + 1] == '\0')
                {
                    position.X--;

                    position.Y++;

                    Visualise();

                    continue;
                }

                if (_map[position.X + 1, position.Y + 1] == '\0')
                {
                    position.X++;

                    position.Y++;
                    
                    Visualise();

                    continue;
                }

                if (position.X == 500 && position.Y == 0)
                {
                    return units;
                }

                _map[position.X, position.Y] = 'o';
                
                units++;

                if (_visualiser != null)
                {
                    toRemove.Add(position);
                }
                else
                {
                    positions[0] = new Point(500, 0);
                }
            }

            if (_visualiser != null)
            {
                foreach (var point in toRemove)
                {
                    positions.Remove(point);
                }

                toRemove.Clear();
            }
        }
    }

    protected void AddFloor()
    {
        for (var x = 0; x < Width; x++)
        {
            _map[x, _maxY + 2] = '#';
        }

        _hasFloor = true;
    }
}