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
    
    private int CountNonSupportingBricks()
    {
        var result = 0;

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

            result += 1 - SettleBricks(Map, false);
        
            Array.Copy(copy, Map, MaxHeight * 100);
        }

        return result;
    }
}