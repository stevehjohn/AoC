using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._21;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        return "Unknown";
    }

    private int Walk((int X, int Y) start, int maxSteps)
    {
        var positions = new HashSet<(int X, int Y, int Ux, int Uy)>
        {
            (start.X, start.Y, 0, 0)
        };

        for (var i = 0; i < maxSteps; i++)
        {
            var newPositions = new HashSet<(int X, int Y, int Ux, int Uy)>();

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

    private void Move(HashSet<(int X, int Y, int Ux, int Uy)> positions, (int X, int Y, int Ux, int Uy) position, int dX, int dY)
    {
        position = (position.X + dX, position.Y + dY, position.Ux, position.Uy);

        if (position.X < 0)
        {
            position.X = Width - 1;
            position.Ux--;
            
            return;
        }

        if (Map[position.X, position.Y] == '#')
        {
            return;
        }
        
        positions.Add(position);
    }
}