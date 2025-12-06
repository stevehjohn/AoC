using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2025._06;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var operators = Input[^1].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

        var width = operators.Length;
        
        var numbers = new int[width][];

        for (var y = 0; y < Input.Length - 1; y++)
        {
            numbers[y] = new int[width];

            var numberStrings = Input[y].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            for (var x = 0; x < width; x++)
            {
                numbers[y][x] = int.Parse(numberStrings[x]);
            }
        }

        var total = 0L;

        var rows = Input.Length - 1;
        
        for (var x = 0; x < width; x++)
        {
            var result = (long) numbers[0][x];

            var isAddition = operators[x] == "+";

            for (var y = 1; y < rows; y++)
            {
                if (isAddition)
                {
                    result += numbers[y][x];
                }
                else
                {
                    result *= numbers[y][x];
                }
            }

            total += result;
        }

        return total.ToString();
    }
}