using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._24;

[UsedImplicitly]
public class Part1 : Base
{
    private readonly HashSet<int> _previousStates = new();

    public override string GetAnswer()
    {
        var grid = ParseInput();

        _previousStates.Add(GetBiodiversity(grid));

        while (true)
        {
            PlayRound(grid);

            var bioDiversity = GetBiodiversity(grid);

            if (_previousStates.Contains(bioDiversity))
            {
                return bioDiversity.ToString();
            }

            _previousStates.Add(bioDiversity);
        }
    }

    private static int GetBiodiversity(bool[,] grid)
    {
        var i = 1;

        var diversity = 0;

        for (var y = 1; y < 6; y++)
        {
            for (var x = 1; x < 6; x++)
            {
                if (grid[x, y])
                {
                    diversity += i;
                }

                i *= 2;
            }
        }

        return diversity;
    }
}