using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2020._17;

public abstract class Base : Solution
{
    public override string Description => "Conway cubes";

    protected HashSet<Point4D> ActiveCubes = new();

    protected int XMin;
    
    protected int XMax;
    
    protected int YMin;

    protected int YMax;
    
    protected int ZMin;

    protected int ZMax = 1;
    
    protected int WMin;

    protected int WMax = 1;

    protected void ParseInput()
    {
        XMax = Input[0].Length;

        YMax = Input.Length;

        for (var y = 0; y < XMax; y++)
        {
            for (var x = 0; x < YMax; x++)
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
        XMax++;

        XMin--;

        YMax++;

        YMin--;

        ZMax++;

        ZMin--;

        if (exploreW)
        {
            WMax++;

            WMin--;
        }

        var add = new List<Point4D>();
        
        var remove = new List<Point4D>();

        for (var w = WMin; w < WMax; w++)
        {
            for (var z = ZMin; z < ZMax; z++)
            {
                for (var y = YMin; y < YMax; y++)
                {
                    for (var x = XMin; x < XMax; x++)
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