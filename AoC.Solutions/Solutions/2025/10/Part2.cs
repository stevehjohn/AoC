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
        var buttons = m.Values.Length;
        
        var buttonMasks = new int[buttons];

        for (var b = 0; b < buttons; b++)
        {
            var mask = 0;
            
            var toggles = m.Values[b];
            
            for (var i = 0; i < toggles.Length; i++)
            {
                mask |= 1 << toggles[i];
            }
            buttonMasks[b] = mask;
        }

        return new Machine2(buttonMasks, m.Totals);
    }
}