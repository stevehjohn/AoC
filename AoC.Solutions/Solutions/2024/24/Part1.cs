using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._24;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var result = GetBusValue('z');
        
        return result.ToString();
    }
}