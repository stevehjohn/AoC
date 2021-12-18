using JetBrains.Annotations;
#if DEBUG
using System.Diagnostics;
#endif
using System.Text;

namespace AoC.Solutions.Solutions._2021._13;

[UsedImplicitly]
public class Part2 : Base
{
    public override bool OcrOutput => false;

    public override string GetAnswer()
    {
        ParseInput();

        DoFolds();

        var width = Dots.Max(d => d.X) + 1;

        var height = Dots.Max(d => d.Y) + 1;

        var image = new char[width, height];

        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                image[x, y] = ' ';
            }
        }

        foreach (var dot in Dots)
        {
            image[dot.X, dot.Y] = '*';
        }

        var answer = new StringBuilder();

        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
#if DEBUG
                if (Debugger.IsAttached)
                {
                    Debug.Write(image[x, y]);
                }
#endif

                answer.Append(image[x, y]);
            }

#if DEBUG
            if (Debugger.IsAttached)
            {
                Debug.WriteLine(string.Empty);
            }
#endif
        }

        return answer.ToString();
    }
}