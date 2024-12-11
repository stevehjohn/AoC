using AoC.Solutions.Extensions;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._11;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();
        
        var i = 0;
        
        75.Repetitions(() =>
        {
            Console.WriteLine($"{i}: {Stones.Count}");
            
            Blink();

            i++;
        });
        
        return Stones.Count.ToString();
    }
}