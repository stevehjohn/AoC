using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._13;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var i = 0;

        var tokens = 0L;
        
        var machine = ParseMachine(i);

        while (machine != null)
        {
            var result = GetButtonPresses(machine);

            tokens += result.A * 3 + result.B;

            i++;
            
            machine = ParseMachine(i);
        }
        
        return tokens.ToString();
    }
}