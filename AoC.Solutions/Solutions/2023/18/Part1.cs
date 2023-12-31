using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._18;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();
        
        return GetArea().ToString();
    }

    private void ParseInput()
    {
        long x = 0, y = 0;

        foreach (var line in Input)
        {
            var parts = line.Split(' ');

            var length = int.Parse(parts[1]);

            Length += length;
            
            switch (parts[0][0])
            {
                case 'R':
                    x += length;
                    break;
                
                case 'D':
                    y += length;
                    break;
                
                case 'L':
                    x -= length;
                    break;
                
                case 'U':
                    y -= length;
                    break;
                
            }
            
            Points.Add((x, y));
        }
    }
}