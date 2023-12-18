using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._18;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var points = ParseInput();
        
        return "Unknown";
    }

    private List<(int X, int Y)> ParseInput()
    {
        int x = 0, y = 0;

        var points = new List<(int X, int Y)> { (0, 0) };

        foreach (var line in Input)
        {
            var parts = line.Split(' ');

            var length = Convert.ToInt32(parts[2][2..7], 16);

            var direction = parts[2][7];

            switch (direction)
            {
                case '0':
                    x += length;
                    break;
                
                case '1':
                    y += length;
                    break;
                
                case '2':
                    x -= length;
                    break;
                
                case '3':
                    y -= length;
                    break;
                
            }
            
            points.Add((x, y));
        }
        
        return points;
    }
}