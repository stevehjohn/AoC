using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._22;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();
        
        SettleBricks();

        var state = Bricks.ToList();
        
        var result = Bricks.Count - CountSupportingBricks();
        
        return result.ToString();
    }
}