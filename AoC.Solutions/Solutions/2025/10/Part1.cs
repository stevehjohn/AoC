using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2025._10;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var result = 0;
        
        foreach (var machine in Machines)
        {
            var presses = machine.SwitchOn();
            
            result += presses;
        }
        
        return result.ToString();
    }
}