using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._21;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var start = ParseInput();

        var result = Walk(start);
        
        return result.ToString();
    }

    private int Walk((int X, int Y) start)
    {
        var positions = new HashSet<(int X, int Y, int Ux, int Uy)>
        {
            (start.X, start.Y, 0, 0)
        };

        var universes = new HashSet<(int Ux, int Uy)>();

        var previousUniverses = 1;

        var step = 1;
        
        while (true)
        {
            var newPositions = new HashSet<(int X, int Y, int Ux, int Uy)>();

            foreach (var position in positions)
            {
                Move(newPositions, position, -1, 0, universes);
            
                Move(newPositions, position, 1, 0, universes);
            
                Move(newPositions, position, 0, -1, universes);
            
                Move(newPositions, position, 0, 1, universes);
            }
            
            positions = newPositions;

            if (universes.Count > previousUniverses)
            {
                Console.WriteLine($"{step}: {positions.Count}");
                
                previousUniverses = universes.Count;
            }

            if (universes.Count > 12)
            {
                break;
            }

            step++;
        }

        foreach (var position in positions.GroupBy(p => new { p.Ux, p.Uy }))
        {
            Console.WriteLine($"{position.Key.Ux}, {position.Key.Uy}: {position.Count()}");
        }

        return positions.Count;
    }

    private void Move(HashSet<(int X, int Y, int Ux, int Uy)> positions, (int X, int Y, int Ux, int Uy) position, int dX, int dY, HashSet<(int Ux, int Uy)> universes)
    {
        position = (position.X + dX, position.Y + dY, position.Ux, position.Uy);

        if (position.X < 0)
        {
            position.X = Width - 1;
            position.Ux--;
            
            universes.Add((position.Ux, position.Uy));
        }

        if (position.X == Width)
        {
            position.X = 0;
            position.Ux++;
            
            universes.Add((position.Ux, position.Uy));
        }

        if (position.Y < 0)
        {
            position.Y = Height - 1;
            position.Uy--;
            
            universes.Add((position.Ux, position.Uy));
        }

        if (position.Y == Height)
        {
            position.Y = 0;
            position.Uy++;
            
            universes.Add((position.Ux, position.Uy));
        }

        if (Map[position.X, position.Y] == '#')
        {
            return;
        }

        positions.Add(position);
    }
}