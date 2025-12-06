using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2025._06;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var numbers = new List<int[]>();

        for (var i = 0; i < Input.Length - 1; i++)
        {
            numbers.Add(Input[i].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray());
        }

        var operators = Input[^1].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

        var width = operators.Length;

        var total = 0L;

        for (var x = 0; x < width; x++)
        {
            var result = (long) numbers[0][x];

            for (var y = 1; y < numbers.Count; y++)
            {
                switch (operators[x])
                {
                    case "+":
                        result += numbers[y][x];
                        
                        break;
                    
                    default:
                        result *= numbers[y][x];
                        
                        break;
                }
            }

            total += result;
        }

        return total.ToString();
    }
}