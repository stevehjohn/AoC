using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2020._17;

public abstract class Base : Solution
{
    public override string Description => "Conway cubes";

    protected readonly HashSet<Point4D> ActiveCubes = new();

    private int _xMin;

    private int _xMax;

    private int _yMin;

    private int _yMax;

    private int _zMin;

    private int _zMax = 1;

    private int _wMin;

    private int _wMax = 1;

    protected void ParseInput()
    {
        _xMax = Input[0].Length;

        _yMax = Input.Length;

        for (var y = 0; y < _xMax; y++)
        {
            for (var x = 0; x < _yMax; x++)
            {
                if (Input[y][x] == '#')
                {
                    ActiveCubes.Add(new Point4D(x, y));
                }
            }
        }
    }

    protected void RunCycle(bool exploreW = false)
    {
        _xMax++;

        _xMin--;

        _yMax++;

        _yMin--;

        _zMax++;

        _zMin--;

        if (exploreW)
        {
            _wMax++;

            _wMin--;
        }

        var add = new List<Point4D>();
        
        var remove = new List<Point4D>();

        for (var w = _wMin; w < _wMax; w++)
        {
            for (var z = _zMin; z < _zMax; z++)
            {
                for (var y = _yMin; y < _yMax; y++)
                {
                    for (var x = _xMin; x < _xMax; x++)
                    {
                        var position = new Point4D(x, y, z, w);

                        var neighbors = CountNeighbors(position);

                        var cube = ActiveCubes.Contains(position);

                        if (cube)
                        {
                            if (neighbors != 2 && neighbors != 3)
                            {
                                remove.Add(position);
                            }
                        }
                        else
                        {
                            if (neighbors == 3)
                            {
                                add.Add(position);
                            }
                        }
                    }
                }
            }
        }

        foreach (var point in remove)
        {
            ActiveCubes.Remove(point);
        }

        foreach (var item in add)
        {
            ActiveCubes.Add(item);
        }
    }

    private int CountNeighbors(Point4D point)
    {
        var neighbors = 0;

        foreach (var p in ActiveCubes)
        {
            neighbors += p.X >= point.X - 1 && p.X <= point.X + 1 &&
                         p.Y >= point.Y - 1 && p.Y <= point.Y + 1 &&
                         p.Z >= point.Z - 1 && p.Z <= point.Z + 1 &&
                         p.W >= point.W - 1 && p.W <= point.W + 1 &&
                         ! (p.X == point.X && p.Y == point.Y && p.Z == point.Z && p.W == point.W)
                             ? 1
                             : 0;

            if (neighbors > 4)
            {
                return 4;
            }
        }
        
        return neighbors;
    }
}