using AoC.Solutions.Infrastructure;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._18;

[UsedImplicitly]
public class Part2 : Base
{
    private readonly IVisualiser<PuzzleState> _visualiser;

    public Part2()
    {
    }

    public Part2(IVisualiser<PuzzleState> visualiser)
    {
        _visualiser = visualiser;
    }
    
    public override string GetAnswer()
    {
        ParseInput();

        ModifyMap();

        FindItemLocations();

        Visualise();
        
        InterrogateMap();

        var result = FindShortestPath();

        return result.ToString();
    }

    private void Visualise(string path = null)
    {
        if (_visualiser != null)
        {
            _visualiser.PuzzleStateChanged(new PuzzleState { Map = Map, Path = path, Paths = Paths });
        }
    }

    private int FindShortestPath()
    {
        var graphs = new Graph[4];

        for (var i = 0; i < 4; i++)
        {
            graphs[i] = new Graph();
        }

        graphs[0].Build(Distances.Where(d => GetQuadrant(d.Key[0]) == 1 && GetQuadrant(d.Key[1]) == 1).ToDictionary(kvp => kvp.Key, kvp => kvp.Value), Doors);
        graphs[1].Build(Distances.Where(d => GetQuadrant(d.Key[0]) == 2 && GetQuadrant(d.Key[1]) == 2).ToDictionary(kvp => kvp.Key, kvp => kvp.Value), Doors);
        graphs[2].Build(Distances.Where(d => GetQuadrant(d.Key[0]) == 3 && GetQuadrant(d.Key[1]) == 3).ToDictionary(kvp => kvp.Key, kvp => kvp.Value), Doors);
        graphs[3].Build(Distances.Where(d => GetQuadrant(d.Key[0]) == 4 && GetQuadrant(d.Key[1]) == 4).ToDictionary(kvp => kvp.Key, kvp => kvp.Value), Doors);

        var solver = new GraphSolver(graphs);
        
        var result = solver.Solve();

        Visualise(result.Path);
        
        return result.Steps;
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