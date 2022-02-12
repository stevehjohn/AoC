using System.Text;
using AoC.Solutions.Common.Ocr;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2016._08;

[UsedImplicitly]
public class Part2 : Base
{
    public override Variant? OcrOutput => Variant.Small;

    public override string GetAnswer()
    {
        Solve();

        var image = CreateImage();

        return image;
    }

    private string CreateImage()
    {
        var image = new StringBuilder();

        for (var y = 0; y < 6; y++)
        {
            for (var x = 0; x < 50; x++)
            {
                image.Append(Screen[x, y] ? '*' : ' ');
            }

            image.Append('\0');
        }

        return image.ToString();
    }
}