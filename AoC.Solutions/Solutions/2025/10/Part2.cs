using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2025._10;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var sum = 0;
        
        foreach (var line in Input)
        {
            var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            var buttonTokens = parts[1..^1];

            var joltageToken = parts[^1];

            var buttons = buttonTokens
                .Select(t => t[1..^1]
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray())
                .ToArray();

            var joltages = joltageToken[1..^1]
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var matrix =  new Matrix(buttons, joltages);
            
            var machine = ToMachine(matrix);
            
            var solver = new Solver(machine);
            
            sum += solver.Solve();
        }

        return sum.ToString();
    }
    
    private static Machine2 ToMachine(Matrix m)
    {
        int buttons = m.Values.Length;
        int jolts = m.Totals.Length;

        var buttonMasks = new long[buttons];

        for (int b = 0; b < buttons; b++)
        {
            long mask = 0;
            var toggles = m.Values[b];
            for (int i = 0; i < toggles.Length; i++)
            {
                mask |= 1L << toggles[i];
            }
            buttonMasks[b] = mask;
        }

        // target isn't needed for part 2; you can pass 0
        return new Machine2(
            length: jolts,
            target: 0L,
            buttons: buttonMasks,
            joltage: m.Totals
        );
    }
}