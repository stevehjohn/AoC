using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2020._17;

public abstract class Base : Solution
{
    public override string Description => "Conway cubes";

    protected List<Point4D> ActiveCubes = new();

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

        var flip = new List<Point4D>();

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

                        var cube = ActiveCubes.SingleOrDefault(c => c.Equals(position));

                        if (cube != null)
                        {
                            if (neighbors != 2 && neighbors != 3)
                            {
                                flip.Add(cube);
                            }
                        }
                        else
                        {
                            if (neighbors == 3)
                            {
                                flip.Add(position);
                            }
                        }
                    }
                }
            }
        }

        foreach (var point in flip)
        {
            var cube = ActiveCubes.SingleOrDefault(c => c.Equals(point));

            if (cube == null)
            {
                ActiveCubes.Add(point);
            }
            else
            {
                ActiveCubes.Remove(cube);
            }
        }
    }

    private int CountNeighbors(Point4D point)
    {
        var neighbors = ActiveCubes.Where(p => p.X >= point.X - 1 && p.X <= point.X + 1 &&
                                               p.Y >= point.Y - 1 && p.Y <= point.Y + 1 &&
                                               p.Z >= point.Z - 1 && p.Z <= point.Z + 1 &&
                                               p.W >= point.W - 1 && p.W <= point.W + 1 &&
                                               ! (p.X == point.X && p.Y == point.Y && p.Z == point.Z && p.W == point.W));

        return neighbors.Count();
    }
}