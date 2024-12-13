using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._13;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var i = 0;

        var tokens = 0;
        
        var machine = ParseMachine(i);

        while (machine != null)
        {
            machine = ParseMachine(i);
            
            i++;
        }
        
        return tokens.ToString();
    }
}