using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._21;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var start = ParseInput();

        var plots = Walk(start, 50);
        
        return plots.ToString();
    }

    private int Walk((int X, int Y) start, int maxSteps)
    {
        var positions = new List<(int X, int Y, int Ux, int Uy)>
        {
            (start.X, start.Y, 0, 0)
        };

        for (var i = 0; i <= maxSteps; i++)
        {
            //if (i % 10 == 0)
            {
                Console.WriteLine($"{i}: {positions.Count}");
            }

            var newPositions = new List<(int X, int Y, int Ux, int Uy)>();

            foreach (var position in positions)
            {
                Move(newPositions, position.X, position.Y, position.Ux, position.Uy, -1, 0);
                
                Move(newPositions, position.X, position.Y, position.Ux, position.Uy, 1, 0);
                
                Move(newPositions, position.X, position.Y, position.Ux, position.Uy, 0, -1);
                
                Move(newPositions, position.X, position.Y, position.Ux, position.Uy, 0, 1);
            }

            positions = newPositions;
        }

        return positions.Count;
    }

    private void Move(List<(int X, int Y, int Ux, int Uy)> positions, int x, int y, int uX, int uY, int dX, int dY)
    {
        x += dX;

        y += dY;

        if (x < 0)
        {
            x = Width - 1;

            uX--;
        }

        if (x == Width)
        {
            x = 0;

            uX++;
        }

        if (y < 0)
        {
            y = Height - 1;

            uY--;
        }

        if (y == Height)
        {
            y = 0;

            uX++;
        }

        if (positions.Contains((x, y, uX, uY)))
        {
            return;
        }

        if (Map[x, y] == '.')
        {
            positions.Add((x, y, uX, uY));
        }
    }
}