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

        return "TESTING";
    }

    private void RunCycle()
    {
        var flip = new List<Point>();

        foreach (var cube in ActiveCubes)
        {
            
        }

        foreach (var point in flip)
        {
            if (ActiveCubes.Contains(point))
            {
                ActiveCubes.Remove(point);
            }
            else
            {
                ActiveCubes.Add(point);
            }
        }
    }
}