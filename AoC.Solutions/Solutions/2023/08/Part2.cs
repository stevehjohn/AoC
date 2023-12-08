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
        var nodes = Map.Where(n => n.Key.EndsWith('A')).Select(n => n.Key).ToArray();

        var pathLengths = new List<long>();
        
        foreach (var node in nodes)
        {
            pathLengths.Add(WalkMap(node));   
        }

        return pathLengths;
    }
}