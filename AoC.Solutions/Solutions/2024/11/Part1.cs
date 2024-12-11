using AoC.Solutions.Extensions;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._11;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var i = 1;
        
        25.Repetitions(() =>
        {
            Console.WriteLine(i);
            
            Blink();

            i++;
        });
        
        return Stones.Count.ToString();   
    }
}