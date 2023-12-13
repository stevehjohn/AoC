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

        var pathLengths = GetPathLengths().ToArray();

        Parallel.Invoke(
            () => pathLengths[0] = Maths.LowestCommonMultiple(pathLengths[0], pathLengths[1]),
            () => pathLengths[1] = Maths.LowestCommonMultiple(pathLengths[2], pathLengths[3]),
            () => pathLengths[2] = Maths.LowestCommonMultiple(pathLengths[4], pathLengths[5])
        );

        Parallel.Invoke(
            () => pathLengths[0] = Maths.LowestCommonMultiple(pathLengths[0], pathLengths[1]),
            () => pathLengths[1] = Maths.LowestCommonMultiple(pathLengths[2], pathLengths[3])
        );
        
        return Maths.LowestCommonMultiple(pathLengths[0], pathLengths[1]).ToString();
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