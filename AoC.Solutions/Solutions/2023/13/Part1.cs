using System.Text;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._13;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var pattern = new List<string>();

        var sum = 0;
        
        foreach (var line in Input)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                sum += AnalysePattern(pattern);
                
                pattern.Clear();
                
                continue;
            }
            
            pattern.Add(line);
        }

        if (pattern.Count > 0)
        {
            sum += AnalysePattern(pattern);
        }

        return sum.ToString();
    }

    private int AnalysePattern(List<string> lines)
    {
        var result = GetVerticalReflectionPoint(lines) * 100;
        
        if (result == 0)
        {
            lines = RotateRight(lines);
            
            result = GetVerticalReflectionPoint(lines);
            
            return result;
        }

        return result;
    }

    private int GetVerticalReflectionPoint(List<string> lines)
    {
        var y = lines.Count / 2;

        while (y < lines.Count - 1)
        {
            if (IsReflectionPoint(lines, y))
            {
                return y + 1;
            }

            y++;
        }

        return 0;
    }

    private bool IsReflectionPoint(List<string> lines, int start)
    {
        var top = start;

        var bottom = start + 1;

        while (top >= 0 && bottom < lines.Count)
        {
            if (! lines[top].Equals(lines[bottom]))
            {
                return false;
            }

            top--;

            bottom++;
        }

        return true;
    }

    private List<string> RotateRight(List<string> lines)
    {
        var rotate = new List<string>();

        for (var x = 0; x < lines[0].Length; x++)
        {
            var line = new StringBuilder();
            
            for (var y = lines.Count - 1; y >= 0; y--)
            {
                line.Append(lines[y][x]);
            }

            rotate.Add(line.ToString());
        }
        
        return rotate;
    }
}