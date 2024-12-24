using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._24;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var result = 0UL;
        
        for (var i = 0; i <= MaxZ; i++)
        {
            if (GetWireValue($"z{i:D2}"))
            {
                result |= 1UL << i;
            }
        }

        return result.ToString();
    }
}