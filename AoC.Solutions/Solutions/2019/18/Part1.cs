﻿using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._18;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        FindItemLocations();

        InterrogateMap();

        var result = FindShortestPath();

        return result.ToString();
    }

    private int FindShortestPath()
    {
        var graph = new Graph();

        graph.Build(Distances, Doors);

        var solver = new GraphSolver([graph]);

        var result = solver.Solve();

        return result.Steps;
    }
}