using System.Text;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._15;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var steps = ParseInput();

        var boxes = new List<(string Label, int Lense)>[256];

        for (var i = 0; i < 256; i++)
        {
            boxes[i] = new List<(string Label, int Lense)>();
        }

        foreach (var step in steps)
        {
            var task = ParseStep(step);
        }
        
        return "Unknown";
    }

    private (string Label, int FocalLength) ParseStep(string step)
    {
        var label = new StringBuilder();

        int i;

        char c = '\0';
        
        for (i = 0; i < step.Length; i++)
        {
            c = step[i];
            
            if (! char.IsLetter(c))
            {
                break;
            }

            label.Append(c);
        }

        if (c == '-')
        {
            return (label.ToString(), 0);
        }

        return (label.ToString(), step[i + 1] - '0');
    }
}