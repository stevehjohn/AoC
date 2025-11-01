using System.Text;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2023._13;

public abstract class Base : Solution
{
    public override string Description => "Point of incidence";
    
    protected string GetAnswerInternal()
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
        }

        return result;
    }

    private int GetVerticalReflectionPoint(List<string> lines)
    {
        var y = 0;

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

    protected abstract bool IsReflectionPoint(List<string> lines, int start);
    
    private static List<string> RotateRight(List<string> lines)
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