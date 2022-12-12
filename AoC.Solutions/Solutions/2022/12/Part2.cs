using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2022._12;

public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var steps = FindShortestPath2();

        return steps.ToString();

        //var starts = new List<Point>();

        //for (var y = 0; y < Height; y++)
        //{
        //    for (var x = 0; x < Width; x++)
        //    {
        //        if (Map[x, y] == 0)
        //        {
        //            starts.Add(new Point(x, y));
        //        }
        //    }
        //}

        //var min = int.MaxValue;

        //foreach (var start in starts)
        //{
        //    var steps = FindShortestPath(start);

        //    if (steps < min)
        //    {
        //        min = steps;
        //    }
        //}

        //return min.ToString();
    }
}