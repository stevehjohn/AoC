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

        Parallel.ForEach(Machines, machine =>
        {
            var presses = machine.ConfigureJoltage();

            Interlocked.Increment(ref count);

            Interlocked.Add(ref result, presses);

            Console.WriteLine($"{count}: {presses}. Total: {result}");
        });
        
        return result.ToString();
    }
}