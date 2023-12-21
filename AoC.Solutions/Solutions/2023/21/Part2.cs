using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._21;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var start = ParseInput();

        var plots = Walk(start, 5000);
        
        return plots.ToString();
    }

    private long Walk((int X, int Y) start, int maxSteps)
    {
        var positions = new List<(int X, int Y, int Ux, int Uy)>
        {
            (start.X, start.Y, 0, 0)
        };

        var history = new List<int>();

        var details = new List<(int Step, int Count, List<int> Counts)>();
        
        for (var i = 0; i <= maxSteps; i++)
        {
            var newPositions = new List<(int X, int Y, int Ux, int Uy)>();

            foreach (var position in positions)
            {
                Move(newPositions, position.X, position.Y, position.Ux, position.Uy, -1, 0);
                
                Move(newPositions, position.X, position.Y, position.Ux, position.Uy, 1, 0);
                
                Move(newPositions, position.X, position.Y, position.Ux, position.Uy, 0, -1);
                
                Move(newPositions, position.X, position.Y, position.Ux, position.Uy, 0, 1);
            }

            positions = newPositions;

            // if ((i + 1) % 50 == 0)
            // {
            //     Console.WriteLine($"{i + 1}: {positions.Count}");
            // }
            //Dump(positions);

            var countInUniverseOne = positions.Count(p => p.Ux == 0 && p.Uy == 0);
            
            if (history.Contains(countInUniverseOne))
            {
                Console.WriteLine($"{i}: {countInUniverseOne}, {history.Last()}");
                
                var universeCounts = positions.GroupBy(p => new { p.Ux, p.Uy }).ToList();

                foreach (var count in universeCounts)
                {
                    Console.WriteLine(count.Count());
                }

                details.Add((i, countInUniverseOne, universeCounts.Where(c => c.Count() != countInUniverseOne).Select(c => c.Count()).ToList()));
                
                if (details.Count > 1)
                {
                    break;
                }
            }
            
            history.Add(countInUniverseOne);
        }

        var target = 26501365 % 2;

        var detail = details[target];

        var m = 26501365L / detail.Step * 8;

        m *= detail.Count;
        
        return m;
    }

    private void Dump(List<(int X, int Y, int Ux, int Uy)> positions)
    {
        for (var uY = -1; uY < 2; uY++)
        for (var y = 0; y < Height; y++)
        {
            for (var uX = -1; uX < 2; uX++)
            {
                Console.ForegroundColor = (uY * 3 + uX) % 2 == 0 ? ConsoleColor.Green : ConsoleColor.Blue;
                
                for (var x = 0; x < Width; x++)
                {
                    if (! positions.Any(p => p.X == x && p.Y == y && p.Ux == uX && p.Uy == uY))
                    {
                        Console.Write(Map[x, y]);

                        continue;
                    }

                    Console.Write('O');
                }
            }

            Console.WriteLine();
        }
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

            uY++;
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