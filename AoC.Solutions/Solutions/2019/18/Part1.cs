using JetBrains.Annotations;

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

    public int FindShortestPath()
    {
        var graph = new Graph();

        graph.Build(Distances, Doors);

        var solver = new GraphSolver(new[] { graph });

        var result = solver.Solve();

#if DUMP && DEBUG
        var pathToVisualise = result.Path.Where(c => ! char.IsUpper(c)).ToArray();

        Visualiser.ShowSolution(new string(pathToVisualise), Paths, ItemLocations);
#endif

        return result.Steps;
    }
}