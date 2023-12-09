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

        var pathLengths = GetPathLengths();
        
        return Maths.LowestCommonMultiple(pathLengths.ToList()).ToString();
    }

    private ConcurrentBag<long> GetPathLengths()
    {
        var pathLengths = new ConcurrentBag<long>();

        Parallel.ForEach(Starts, new ParallelOptions { MaxDegreeOfParallelism = 6 } ,node =>
        {
            pathLengths.Add(WalkMap(node));
        });

        return pathLengths;
    }
}