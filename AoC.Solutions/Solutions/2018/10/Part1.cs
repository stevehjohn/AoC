using System.Text;
using AoC.Solutions.Common.Ocr;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2018._10;

[UsedImplicitly]
public class Part1 : Base
{
    public override Variant? OcrOutput => Variant.Large;

    public override string GetAnswer()
    {
        Solve();

        return ResultToString();
    }

    protected string ResultToString()
    {
        var xMin = Stars.Min(s => s.Position.X);

        var yMin = Stars.Min(s => s.Position.Y);

        var xMax = Stars.Max(s => s.Position.X);

        var yMax = Stars.Max(s => s.Position.Y);

        var matrix = new char[xMax - xMin + 1, yMax - yMin + 1];

        foreach (var star in Stars)
        {
            matrix[star.Position.X - xMin, star.Position.Y - yMin] = '*';
        }

        var builder = new StringBuilder();

        for (var y = 0; y < yMax - yMin + 1; y++)
        {
            for (var x = 0; x < xMax - xMin + 1; x++)
            {
                builder.Append(matrix[x, y] == '*' ? '*' : ' ');
            }

            builder.Append('\0');
        }

        return builder.ToString();
    }
}