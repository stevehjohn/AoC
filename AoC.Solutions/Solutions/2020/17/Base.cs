using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2020._17;

public abstract class Base : Solution
{
    public override string Description => "Conway cubes";

    protected List<Point> ActiveCubes = new();

    protected int XMin;
    
    protected int XMax;
    
    protected int YMin;

    protected int YMax;
    
    protected int ZMin;

    protected int ZMax = 1;

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
                    ActiveCubes.Add(new Point(x, y));
                }
            }
        }
    }
}