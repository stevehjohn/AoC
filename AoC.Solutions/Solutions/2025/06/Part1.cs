using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2025._06;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var operators = Input[^1].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

        var width = operators.Length;
        
        var rows = Input.Length - 1;

        var totals = new long[width];

        for (var y = 0; y < rows; y++)
        {
            var numberStrings = Input[y].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            for (var x = 0; x < width; x++)
            {
                var number = long.Parse(numberStrings[x]);
                
                if (y == 0)
                {
                    totals[x] = number;
                    
                    continue;
                }

                if (operators[x] == "+")
                {
                    totals[x] += number;
                }
                else
                {
                    totals[x] *= number;
                }
            }
        }

        var total = 0L;
        
        for (var x = 0; x < width; x++)
        {
            total += totals[x];
        }

        return total.ToString();
    }
}