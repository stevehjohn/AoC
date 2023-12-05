using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._05;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        for (var i = 0; i < Seeds.Length; i++)
        {
            Seeds[i] = RemapSeed(Seeds[i]);
        }
        
        return Seeds.Min().ToString();
    }
}