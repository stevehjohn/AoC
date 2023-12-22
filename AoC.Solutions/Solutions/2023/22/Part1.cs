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
    
    private int CountSupportingBricks()
    {
        var count = 0;

        var settledState = Bricks.ToList();

        foreach (var brick in settledState)
        {
            Bricks.Remove(brick);

            count += SettleBricks(false) ? 1 : 0;

            Bricks.Add(brick);
        }
        
        return count;
    }
}