using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._20;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var presses = 0;
        
        // while (true)
        // {
        //     presses++;
        //
        //     SendPulses();
        //
        //     if (CheckAllConjunctionsOff())
        //     {
        //         break;
        //     }
        // }
        
        return presses.ToString();
    } 
}