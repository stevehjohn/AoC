using System.Collections.Concurrent;
using AoC.Solutions.Libraries;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._08;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var pathLengths = GetPathLengths().ToList();

        return Maths.LowestCommonMultiple(pathLengths).ToString();
    }

    private ConcurrentBag<long> GetPathLengths()
    {
        var pathLengths = new ConcurrentBag<long>();

        Starts
            .AsParallel()
            .WithDegreeOfParallelism(6)
            .ForAll(node => pathLengths.Add(WalkMap(node)));
        
        return pathLengths;
    }
}