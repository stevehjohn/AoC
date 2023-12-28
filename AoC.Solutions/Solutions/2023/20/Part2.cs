using System.Collections.Concurrent;
using AoC.Solutions.Libraries;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._20;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var baseMachine = new Machine();
        
        baseMachine.ParseInput(Input);
        
        var penultimateConjunctions = baseMachine.GetAllPenultimateConjunctions();

        var iterationsToReceiveLow = new ConcurrentBag<long>();

        Parallel.ForEach(penultimateConjunctions, conjunction =>
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
}