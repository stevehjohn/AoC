using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._11;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var result = 0L;

        Parallel.For(0, Stones.Length, i =>
        {
            Interlocked.Add(ref result, Blink(Stones[i], 25));
        });

        return result.ToString();
    }
}