using AoC.Solutions.Common;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2020._17;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        for (var i = 0; i < 6; i++)
        {
            RunCycle();
        }

        return ActiveCubes.Count.ToString();
    }

    private void RunCycle()
    {
        XMax++;

        XMin--;

        YMax++;

        YMin--;

        ZMax++;

        ZMin--;

        var flip = new List<Point>();

        for (var z = ZMin; z < ZMax; z++)
        {
            for (var y = YMin; y < YMax; y++)
            {
                for (var x = XMin; x < XMax; x++)
                {
                    var position = new Point(x, y, z);

                    var neighbors = CountNeighbors(position);

                    var cube = ActiveCubes.SingleOrDefault(c => c.Equals(position));

                    if (cube != null)
                    {
                        if (neighbors is not 2 or 3)
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

    private int CountNeighbors(Point point)
    {
        var neighbors = ActiveCubes.Where(p => p.X >= point.X - 1 && p.X <= point.X + 1 &&
                                               p.Y >= point.Y - 1 && p.Y <= point.Y + 1 &&
                                               p.Z >= point.Z - 1 && p.Z <= point.Z + 1 &&
                                               ! (p.X == point.X && p.Y == point.Y && p.Z == point.Z));

        return neighbors.Count();
    }
}