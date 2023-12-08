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
        
        return Maths.LowestCommonMultiple(pathLengths).ToString();
    }

    private List<long> GetPathLengths()
    {
        var pathLengths = new List<long>();
        
        foreach (var node in Starts)
        {
            pathLengths.Add(WalkMap(node));   
        }

        return pathLengths;
    }
}