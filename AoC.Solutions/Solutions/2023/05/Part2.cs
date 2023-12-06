using System.Collections.Concurrent;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._05;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var seeds = new ConcurrentBag<long>();
        
        for (var i = 0; i < Seeds.Length; i += 2)
        {
            Parallel.For(Seeds[i], Seeds[i] + Seeds[i + 1] - 1, s => seeds.Add(RemapSeed(s)));
        }
        
        return seeds.Min().ToString();
    }
}