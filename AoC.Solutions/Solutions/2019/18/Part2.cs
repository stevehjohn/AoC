using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._18;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        ModifyMap();

        InterrogateMap();

        FindShortestPath();

        return "TESTING";
    }

    private void FindShortestPath()
    {
        var graphs = new Graph[4];

        for (var i = 0; i < 4; i++)
        {
            graphs[i] = new Graph();
        }

        graphs[0].Build(null, Doors);
    }

    private int GetQuadrant(char item)
    {
        var location = ItemLocations[item];

        if (location.Y < 40)
        {
            if (location.X < 40)
            {
                return 1;
            }

            return 2;
        }

        if (location.X < 40)
        {
            return 3;
        }

        return 4;
    }

    private void ModifyMap()
    {
        Map[39, 39] = '1';
        Map[40, 39] = '#';
        Map[41, 39] = '2';
        Map[39, 40] = '#';
        Map[40, 40] = '#';
        Map[41, 40] = '#';
        Map[39, 41] = '3';
        Map[40, 41] = '#';
        Map[41, 41] = '4';
    }
}