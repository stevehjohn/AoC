﻿#if DEBUG
using System.Diagnostics;
#endif
using System.Text;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._11;

[UsedImplicitly]
public class Part2 : Base
{
    public override bool OcrOutput => true;

    public override string GetAnswer()
    {
        RunRobot(1);

        var minX = Panels.Min(p => p.Position.X);

        var width = Panels.Max(p => p.Position.X) - minX;

        var minY = Panels.Min(p => p.Position.Y);

        var height = Panels.Max(p => p.Position.Y) - minY;

        var image = new char[width + 1, height + 1];

        for (var y = height; y >= 0; y--)
        {
            for (var x = 0; x <= width; x++)
            {
                image[x, y] = ' ';
            }
        }

        foreach (var panel in Panels)
        {
            if (panel.Colour == 1)
            {
                image[panel.Position.X + Math.Abs(minX) - 1, panel.Position.Y + Math.Abs(minY)] = '*';
            }
        }

        var answer = new StringBuilder();

        for (var y = height; y >= 0; y--)
        {
            for (var x = 0; x <= width; x++)
            {
#if DEBUG
                if (Debugger.IsAttached)
                {
                    Debug.Write(image[x, y]);
                }
#endif

                answer.Append(image[x, y]);
            }

            answer = new StringBuilder(answer.ToString().TrimEnd());

            answer.Append('\0');

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
