using AoC.Solutions.Infrastructure;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._22;

[UsedImplicitly]
public class Part2 : Base
{
    public Part2()
    {
    }

    public Part2(IVisualiser<PuzzleState> visualiser) : base(visualiser)
    {
    }

    public override string GetAnswer()
    {
        ParseInput();
        
        SettleBricks(Map);
        
        var result = GetNonSupportingBricks();
        
        var count = 0;
        
        Parallel.ForEach(result,
            () => 0,
            (brickId, _, c) =>
            {
                var copy = new int[MaxHeight, 10, 10];
        
                Array.Copy(Map, copy, MaxHeight * 100);
                
                WalkUpMap((x, y, z) =>
                {
                    if (copy[z, x, y] == brickId)
                    {
                        copy[z, x, y] = 0;
                    }
                });
        
                return c + SettleBricks(copy);
            }, c => Interlocked.Add(ref count, c));

        return count.ToString();
    }
    
    private List<int> GetNonSupportingBricks()
    {
        var result = new List<int>();

        var copy = new int[MaxHeight, 10, 10];
        
        Array.Copy(Map, copy, MaxHeight * 100);
        
        for (var id = 1; id <= Count; id++)
        {
            WalkUpMap((x, y, z) =>
            {
                // ReSharper disable once AccessToModifiedClosure
                if (Map[z, x, y] == id)
                {
                    Map[z, x, y] = 0;
                }
            });

            if (SettleBricks(Map, false) > 0)
            {
                result.Add(id);
            }

            Array.Copy(copy, Map, MaxHeight * 100);
        }

        return result;
    }
}