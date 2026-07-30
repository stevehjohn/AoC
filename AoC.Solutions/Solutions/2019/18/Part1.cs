using AoC.Solutions.Infrastructure;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._18;

[UsedImplicitly]
public class Part1 : Base
{
    private readonly IVisualiser<PuzzleState> _visualiser;

    public Part1()
    {
    }
    
    public Part1(IVisualiser<PuzzleState> visualiser)
    {
        _visualiser = visualiser;
    }

    public override string GetAnswer()
    {
        ParseInput();

        FindItemLocations();

        Visualise();
        
        InterrogateMap();

        var result = FindShortestPath();

        return result.ToString();
    }

    private void Visualise(string path = null)
    {
        _visualiser?.PuzzleStateChanged(new PuzzleState { Map = Map, Path = path, Paths = Paths });
    }

    private int FindShortestPath()
    {
        var graph = new Graph();

        graph.Build(Distances, Doors);

        var solver = new GraphSolver([graph]);

        var result = solver.Solve();
        
        Visualise(result.Path);

        return result.Steps;
    }
}