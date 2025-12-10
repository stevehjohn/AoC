using JetBrains.Annotations;
using Microsoft.Z3;

namespace AoC.Solutions.Solutions._2025._10;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var result = 0;

        var count = -1;

        Parallel.For(0, Input.Length,
            machineId =>
            {
                var machine = new Machine(Input[machineId]);
                
                var presses = machine.ConfigureJoltageDfs();

                Interlocked.Increment(ref count);
        
                Interlocked.Add(ref result, presses);
        
                // Console.WriteLine($"{count}: Line {machineId}: {presses}. Total: {result}.");
            });

        using var ctx = new Context();

        // one Int var per button: x_b >= 0
        var x = new IntExpr[5];
        
        return result.ToString();
    }
}