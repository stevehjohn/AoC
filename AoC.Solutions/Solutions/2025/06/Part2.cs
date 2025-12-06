using System.Text;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2025._06;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var total = 0L;

        var width = Input[0].Length;

        var height = Input.Length;

        var numbers = new List<long>();

        var number = new StringBuilder();

        for (var x = width - 1; x >= 0; x--)
        {
            number.Clear();

            for (var y = 0; y < height - 1; y++)
            {
                var digit = Input[y][x];

                if (digit != ' ')
                {
                    number.Append(digit);
                }
            }

            numbers.Add(long.Parse(number.ToString()));

            var operation = Input[height - 1][x];

            if (operation != ' ')
            {
                var result = numbers[0];

                for (var y = 1; y < numbers.Count; y++)
                {
                    switch (operation)
                    {
                        case '+':
                            result += numbers[y];

                            break;

                        default:
                            result *= numbers[y];

                            break;
                    }
                }

                total += result;

                numbers.Clear();

                x--;
            }
        }

        return total.ToString();
    }
}