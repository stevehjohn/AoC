using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._05;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var seeds = new List<long>();
        
        for (var i = 0; i < Seeds.Length; i += 2)
        {
            for (var j = Seeds[i]; j < Seeds[i] + Seeds[i + 1]; j++)
            {
                seeds.Add(RemapSeed(j));
            }
        }
        
        return seeds.Min().ToString();
    }
}