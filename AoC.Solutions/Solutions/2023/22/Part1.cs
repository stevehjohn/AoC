using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._22;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();
        
        SettleBricks();
        
        var result = Bricks.Count - CountSupportingBricks();
        
        return result.ToString();
    }
}