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
                if (position.X > 0 && Map[position.X - 1, position.Y] == '.' && ! newPositions.Contains((position.X - 1, position.Y)))
                {
                    newPositions.Add((position.X - 1, position.Y));
                }

                if (position.X < Width - 1 && Map[position.X + 1, position.Y] == '.' && ! newPositions.Contains((position.X + 1, position.Y)))
                {
                    newPositions.Add((position.X + 1, position.Y));
                }

                if (position.Y > 0 && Map[position.X, position.Y - 1] == '.' && ! newPositions.Contains((position.X, position.Y - 1)))
                {
                    newPositions.Add((position.X, position.Y - 1));
                }

                if (position.Y < Height - 1 && Map[position.X, position.Y + 1] == '.' && ! newPositions.Contains((position.X, position.Y + 1)))
                {
                    newPositions.Add((position.X, position.Y + 1));
                }
            }

            positions = newPositions;
        }

        return positions.Count;
    }
}