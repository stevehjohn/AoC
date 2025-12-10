using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2025._10;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var result = 0;

        var count = 0;

        Parallel.For(0, Machines.Count,
            machineId =>
            {
                var presses = Machines[machineId].ConfigureJoltage();

                Machines[machineId] = null;

                Interlocked.Increment(ref count);

                Interlocked.Add(ref result, presses);

                Console.WriteLine($"{count}: {presses}. Total: {result}");
            });

        return result.ToString();
    }
}