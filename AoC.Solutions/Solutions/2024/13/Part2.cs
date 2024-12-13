using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._13;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var i = 0;

        var tokens = 0L;
        
        var machine = ParseMachine(i);

        while (machine != null)
        {
            var result = GetButtonPresses(machine, true);

            tokens += result.A * 3 + result.B;

            i++;
            
            machine = ParseMachine(i);
        }
        
        return tokens.ToString();
    }
}