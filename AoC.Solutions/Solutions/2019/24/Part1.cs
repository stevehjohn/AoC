using AoC.Solutions.Common;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._24;

[UsedImplicitly]
public class Part1 : Base
{
    private HashSet<int> _previousStates = new();

    private readonly bool[,] _grid = new bool[7, 7];

    public override string GetAnswer()
    {
        ParseInput();

        PlayRound();

        return "TESTING";
    }

    private void PlayRound()
    {
        var dies = new List<Point>();

        var infests = new List<Point>();

        for (var y = 1; y < 6; y++)
        {
            for (var x = 1; x < 6; x++)
            {
            }
        }
    }

    private void ParseInput()
    {
        var y = 1;
        foreach (var line in Input)
        {
            for (var x = 1; x < 6; x++)
            {
                _grid[x, y] = line[x - 1] == '#';
            }

            y++;
        }
    }
}