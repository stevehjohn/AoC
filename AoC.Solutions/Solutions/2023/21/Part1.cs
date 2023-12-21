using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._21;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var start = ParseInput();

        var plots = Walk(start, 64);
        
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
            var newPositions = new List<(int X, int Y)>();

            foreach (var position in positions)
            {
                Move(newPositions, position, -1, 0);
            
                Move(newPositions, position, 1, 0);
            
                Move(newPositions, position, 0, -1);
            
                Move(newPositions, position, 0, 1);
            }
            
            positions = newPositions;
        }

        return positions.Count;
    }

    private void Move(List<(int X, int Y)> positions, (int X, int Y) position, int dX, int dY)
    {
        position = (position.X + dX, position.Y + dY);

        if (position.X < 0 || position.X == Width || position.Y < 0 || position.Y == Height)
        {
            return;
        }

        if (Map[position.X, position.Y] == '#')
        {
            return;
        }

        if (positions.Any(p => p == position))
        {
            return;
        }

        positions.Add((position.X, position.Y));
    }
}