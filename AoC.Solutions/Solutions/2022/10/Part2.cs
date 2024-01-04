using System.Text;
using AoC.Solutions.Common.Ocr;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2022._10;

[UsedImplicitly]
public class Part2 : Base
{
    public override Variant? OcrOutput => Variant.Small;

    public override string GetAnswer()
    {
        var registerX = 1;

        var column = 0;

        var answer = new StringBuilder();

        foreach (var line in Input)
        {
            switch (line[..4])
            {
                case "noop":
                    answer.Append(Math.Abs(registerX - column) < 2 ? '*' : '.');

                    column++;

                    if (column == 40)
                    {
                        column = 0;

                        answer.Append('\0');
                    }

                    break;

                case "addx":
                    answer.Append(Math.Abs(registerX - column) < 2 ? '*' : '.');

                    column++;

                    if (column == 40)
                    {
                        column = 0;

                        answer.Append('\0');
                    }

                    answer.Append(Math.Abs(registerX - column) < 2 ? '*' : '.');

                    column++;

                    if (column == 40)
                    {
                        column = 0;

                        answer.Append('\0');
                    }

                    registerX += int.Parse(line[5..]);

                    break;
            }
        }

        return answer.ToString();
    }
}