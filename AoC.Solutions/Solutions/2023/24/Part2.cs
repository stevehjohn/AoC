using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._24;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var nonIntersecting = FindNonIntersectingStones();

        var h1 = nonIntersecting[0];

        var h2 = nonIntersecting[1];
        
        var y = h1.Position.Y;

        var vY = h1.Velocity.Y;

        return "Unknown";
    }
    
    private List<(DoublePoint Position, DoublePoint Velocity)> FindNonIntersectingStones()
    {
        return Hail.GroupBy(h => new { pY = h.Position.Y, vY = h.Velocity.Y }).Single(g => g.Count() > 1).ToList();
    }
}