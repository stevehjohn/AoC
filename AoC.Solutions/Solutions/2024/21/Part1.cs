using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._21;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var numPad = new NumPad();
        
        Console.WriteLine(numPad.GetSequence("029A"));
        
        return "Unknown";
    }
}