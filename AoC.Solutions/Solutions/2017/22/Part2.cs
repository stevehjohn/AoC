using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2017._22;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var cells = new Dictionary<long, byte>();

        foreach (var point in Infected)
        {
            cells[Key(point.X, point.Y)] = 2;
        }

        var x = Position.X;

        var y = Position.Y;

        var dx = Direction.X;

        var dy = Direction.Y;

        var infections = 0;

        for (var i = 0; i < 10_000_000; i++)
        {
            var key = Key(x, y);

            cells.TryGetValue(key, out var state);

            switch (state)
            {
                case 0:
                    (dx, dy) = (dy, -dx);
                    cells[key] = 1;
                    
                    break;

                case 1:
                    cells[key] = 2;
                    infections++;
                    
                    break;

                case 2:
                    (dx, dy) = (-dy, dx);
                    cells[key] = 3;
                    
                    break;

                case 3:
                    (dx, dy) = (-dx, -dy);
                    cells.Remove(key);
                    
                    break;
            }

            x += dx;

            y += dy;
        }

        return infections.ToString();
    }

    private static long Key(int x, int y)
    {
        return ((long) x << 32) | (uint) y;
    }
}