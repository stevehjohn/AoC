using AoC.Solutions.Infrastructure;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._22;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        SettleBricks(Map);

        var result = CountNonSupportingBricks();
        
        return result.ToString();
    }

    public Part1()
    {
    }

    public Part1(IVisualiser<PuzzleState> visualiser) : base(visualiser)
    {
    }

    private int CountNonSupportingBricks()
    {
        var result = 0;

        var copy = new int[MaxHeight, 10, 10];
        
        Array.Copy(Map, copy, MaxHeight * 100);
        
        for (var id = 1; id <= Count; id++)
        {
            for (var z = 1; z < HighestZ; z++)
            {
                for (var x = 0; x < 10; x++)
                {
                    for (var y = 0; y < 10; y++)
                    {
                        // ReSharper disable once AccessToModifiedClosure
                        if (Map[z, x, y] == id)
                        {
                            Map[z, x, y] = -1;
                        }
                    }
                }
            }

            var count = SettleBricks(Map, false);
            
            result += 1 - count;

            if (count == 0)
            {
                Visualise(false, id);
            }

            Array.Copy(copy, Map, MaxHeight * 100);
        }

        return result;
    }
}