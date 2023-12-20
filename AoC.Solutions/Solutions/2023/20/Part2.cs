using AoC.Solutions.Libraries;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._20;

[UsedImplicitly]
public class Part2 : Base
{
    private readonly List<string> _penultimateConjunctions = new();
    
    public override string GetAnswer()
    {
        ParseInput();
        
        GetAllPenultimateConjunctions();

        var conjunctions = new List<long>();
        
        foreach (var conjunction in _penultimateConjunctions)
        {
            ParseInput();

            var presses = 0;

            while (true)
            {
                presses++;
        
                var result = SendPulses(conjunction);
        
                if (result == (0, 0))
                {
                    conjunctions.Add(presses);
                    
                    break;
                }
            }
        }
        
        return Maths.LowestCommonMultiple(conjunctions).ToString();
    }

    private void GetAllPenultimateConjunctions()
    {
        foreach (var module in Modules.Where(m => m.Value.Targets.Contains("rx")))
        {
            foreach (var item in Modules)
            {
                if (item.Value.Targets.Contains(module.Key))
                {
                    _penultimateConjunctions.Add(item.Key);
                }
            }
        }
    }
}