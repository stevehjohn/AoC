using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._18;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        ModifyMap();

        FindItemLocations();

        InterrogateMap();

        var result = FindShortestPath();

        return result.ToString();
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

#if DEBUG && DUMP
        var pathToVisualise = result.Path.Where(c => ! char.IsUpper(c)).ToArray();

        var quadrantPositions = new Dictionary<int, char>
                                {
                                    { 1, '1' },
                                    { 2, '2' },
                                    { 3, '3' },
                                    { 4, '4' }
                                };

        var quadrant = GetQuadrant(pathToVisualise[1]);

        for (var i = 0; i < pathToVisualise.Length - 1; i++)
        {
            if (char.IsNumber(pathToVisualise[i + 1]))
            {
                quadrant = pathToVisualise[i + 1] - '0';

                continue;
            }

            var subPath = $"{(char.IsNumber(pathToVisualise[i]) ? quadrantPositions[quadrant] : pathToVisualise[i])}{pathToVisualise[i + 1]}";

            Visualiser.ShowSolution(subPath, Paths, ItemLocations, true);

            quadrantPositions[quadrant] = pathToVisualise[i + 1];
        }
#endif

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