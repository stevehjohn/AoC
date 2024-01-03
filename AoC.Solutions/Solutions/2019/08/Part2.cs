using System.Text;
using AoC.Solutions.Common.Ocr;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._08;

[UsedImplicitly]
public class Part2 : Base
{
    public override Variant? OcrOutput => Variant.Small;

    public override string GetAnswer()
    {
        var data = Input[0];

        var image = new string('2', 150).ToCharArray();

        var index = 0;

        while (index < data.Length)
        {
            var layer = data.Skip(index).Take(150).ToArray();

            for (var i = 0; i < 150; i++)
            {
                if (image[i] == '2' && layer[i] < '2')
                {
                    image[i] = layer[i] == '1' ? '*' : ' ';
                }
            }

            index += 150;
        }

        var result = new StringBuilder();

        result.Append($"{new string(image.Take(25).ToArray())}\0");
        result.Append($"{new string(image.Skip(25).Take(25).ToArray())}\0");
        result.Append($"{new string(image.Skip(50).Take(25).ToArray())}\0");
        result.Append($"{new string(image.Skip(75).Take(25).ToArray())}\0");
        result.Append($"{new string(image.Skip(100).Take(25).ToArray())}\0");
        result.Append($"{new string(image.Skip(125).Take(25).ToArray())}\0");

        return result.ToString();
    }
}