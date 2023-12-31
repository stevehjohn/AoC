using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._24;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var grid = ParseInput();

        var previousStates = new HashSet<int>
                             {
                                 grid
                             };

        while (true)
        {
            grid = PlayRound(grid);

            if (! previousStates.Add(grid))
            {
                return grid.ToString();
            }
        }
    }
}