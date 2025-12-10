using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2025._10;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var result = 0;

        var machineId = -1;

        Parallel.For(0, Input.Length,
            new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount },
            _ =>
            {
                Interlocked.Increment(ref machineId);
        
                var machine = new Machine(Input[machineId]);
                
                var presses = machine.ConfigureJoltage();

                Interlocked.Add(ref result, presses);
        
                Console.WriteLine($"{machineId}: {presses}. Total: {result}");
            });

        return result.ToString();
    }
}