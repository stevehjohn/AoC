using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2025._10;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var result = 0;
        
        foreach (var line in Input)
        {
            var presses = new Machine(line).SwitchOn();
            
            result += presses;
        }
        
        return result.ToString();
    }
}