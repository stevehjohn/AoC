using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._18;

[UsedImplicitly]
public class Part2 : Base
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

            var length = Convert.ToInt32(parts[2][2..7], 16);

            Length += length;
            
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
            
            Points.Add((x, y));
        }
    }
}