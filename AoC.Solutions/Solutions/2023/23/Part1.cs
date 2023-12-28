using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._23;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();
        
        CreateEdges();

        FindLongestPath(Intersections[0], 0);

        Console.WriteLine(Counts.Count);

        Console.WriteLine(Counts.IndexOf(Counts.Max()));

        return Counts.Max().ToString();
    }
}