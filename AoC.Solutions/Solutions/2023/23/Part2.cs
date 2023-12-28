using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._23;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();
        
        CreateEdges(true);

        FindLongestPath(Intersections[0], 0, true);

        return Counts.Max().ToString();
    }
}