using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._21;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var start = ParseInput();

        var plots = Walk(start, 26501365);
        
        return plots.ToString();
    }

    private int Walk((int X, int Y) start, int maxSteps)
    {
        var positions = new List<(int X, int Y)>
        {
            (start.X, start.Y)
        };

        for (var i = 0; i < maxSteps; i++)
        {
            if (i % 10 == 0)
            {
                Console.WriteLine($"{maxSteps - i}: {positions.Count}, {positions.Distinct().Count()}");
            }

            var newPositions = new List<(int X, int Y)>();

            foreach (var position in positions)
            {
                Move(newPositions, position.X, position.Y, -1, 0);
                
                Move(newPositions, position.X, position.Y, 1, 0);
                
                Move(newPositions, position.X, position.Y, 0, -1);
                
                Move(newPositions, position.X, position.Y, 0, 1);
            }

            positions = newPositions;
        }

        return positions.Count;
    }

    private void Move(List<(int X, int Y)> positions, int x, int y, int dX, int dY)
    {
        x += dX;

        y += dY;

        if (x < 0)
        {
            x = Width - 1;
        }

        if (x == Width)
        {
            x = 0;
        }

        if (y < 0)
        {
            y = Height - 1;
        }

        if (y == Height)
        {
            y = 0;
        }

        if (positions.Contains((x, y)))
        {
            return;
        }

        if (Map[x, y] == '.')
        {
            positions.Add((x, y));
        }
    }
}