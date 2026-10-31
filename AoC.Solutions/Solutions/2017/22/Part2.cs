using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2017._22;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        const int size = 2048;
        
        const int offset = size / 2;

        var grid = new byte[size * size];

        foreach (var point in Infected)
        {
            grid[(point.Y + offset) * size + point.X + offset] = 2;
        }

        var x = Position.X + offset;
        
        var y = Position.Y + offset;

        var dx = Direction.X;
        
        var dy = Direction.Y;

        var infections = 0;

        for (var i = 0; i < 10_000_000; i++)
        {
            var index = y * size + x;
            
            var state = grid[index];

            switch (state)
            {
                case 0: 
                    (dx, dy) = (dy, -dx);
                    grid[index] = 1;
                    
                    break;

                case 1: 
                    grid[index] = 2;
                    infections++;
                    
                    break;

                case 2: 
                    (dx, dy) = (-dy, dx);
                    grid[index] = 3;
                    
                    break;

                case 3: 
                    (dx, dy) = (-dx, -dy);
                    grid[index] = 0;
                    
                    break;
            }

            x += dx;
            
            y += dy;
        }

        return infections.ToString();
    }
}