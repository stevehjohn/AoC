using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2019._10;

public abstract class Base : Solution
{
    public override string Description => "Asteroids";

    protected List<Point> Asteroids;

    protected Base()
    {
        Asteroids = new List<Point>();

        for (var y = 0; y < Input.Length; y++)
        {
            for (var x = 0; x < Input[y].Length; x++)
            {
                if (Input[y][x] == '#')
                {
                    Asteroids.Add(new Point(x, y));
                }
            }
        }
    }
}