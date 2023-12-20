using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._20;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var presses = 0;

        DumpTargeting("rx");
        
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

    private void DumpTargeting(string target)
    {
        foreach (var module in Modules)
        {
            if (module.Value.Targets.Contains(target) && module.Value.Type == Type.Conjunction)
            {
                Console.WriteLine(module.Key);
                
                DumpTargeting(module.Key);
            }
        }
    }

    private bool CheckAllConjunctionsOff()
    {
        foreach (var module in Modules)
        {
            if (module.Value.Type == Type.Conjunction)
            {
                foreach (var pulse in module.Value.ReceivedPulses)
                {
                    if (pulse.Value)
                    {
                        return false;
                    }
                }
            }
        }
        
        return true;
    }
}