using System.Collections.Concurrent;
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

        var iterationsToReceiveLow = new ConcurrentBag<long>();

        Parallel.ForEach(_penultimateConjunctions, conjunction =>
        {
            var machine = new Machine();

            machine.ParseInput(Input);

            var presses = 0;

            while (true)
            {
                presses++;

                var result = machine.SendPulses(conjunction);

                if (result == (0, 0))
                {
                    iterationsToReceiveLow.Add(presses);

                    break;
                }
            }
        });
        
        return Maths.LowestCommonMultiple(iterationsToReceiveLow.ToList()).ToString();
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