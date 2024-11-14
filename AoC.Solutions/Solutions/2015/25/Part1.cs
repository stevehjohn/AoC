using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2015._25;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var parts = Input[0].Split([' ', ',', '.'], StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

        var targetRow = int.Parse(parts[15]);

        var targetColumn = int.Parse(parts[17]);

        var code = 20151125L;

        var row = 1;

        var column = 1;

        while (row != targetRow || column != targetColumn)
        {
            row -= 1;

            column += 1;

            if (row == 0)
            {
                row = column;

                column = 1;
            }

            code *= 252533;

            code %= 33554393;
        }

        return code.ToString();
    }
}