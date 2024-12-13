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

        var count = 0;
        
        while (machine != null)
        {
            var result = GetButtonPresses(machine);

            if (result.A > 0)
            {
                count++;
            }

            tokens += result.A * 3 + result.B;

            i++;
            
            machine = ParseMachine(i);
        }
        
        Console.WriteLine(count);
        
        return tokens.ToString();
    }
}