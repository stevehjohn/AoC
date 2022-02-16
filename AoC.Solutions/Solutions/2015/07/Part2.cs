using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2015._07;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();
        
        Values.Add("b", 3176);

        var result = FindOutputValue("a");

        return result.ToString();
    }
}