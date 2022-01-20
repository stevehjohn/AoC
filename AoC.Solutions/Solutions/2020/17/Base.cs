using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2020._17;

public abstract class Base : Solution
{
    public override string Description => "Conway cubes";

    protected List<Point> ActiveCubes;

    protected void ParseInput()
    {
        for (var y = 0; y < Input.Length; y++)
        {
            for (var x = 0; x < Input[0].Length; x++)
            {
                if (Input[y][x] == '#')
                {
                    ActiveCubes.Add(new Point(x, y));
                }
            }
        }
    }
}