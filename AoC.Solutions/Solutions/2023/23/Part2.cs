using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._23;

[UsedImplicitly]
public class Part2 : Base
{
    private Edge _start;
    
    public override string GetAnswer()
    {
        ParseInput();
        
        CreateEdges();

        var result = 0;

        return result.ToString();
    }

    private void CreateEdges()
    {
        var queue = new Queue<(int X, int Y, (int Dx, int Dy), int Steps)>();

        var visited = new HashSet<(int X, int Y)> { (1, 0) };
        
        queue.Enqueue((1, 1, South, 1));

        while (queue.TryDequeue(out var position))
        {
            var (x, y, direction, steps) = position;

            var (dX, dY) = direction;
            
            if (! visited.Add((x, y)))
            {
                continue;
            }
            
            var count = 0;

            count += Map[x - 1, y] == '#' ? 1 : 0;
            count += Map[x + 1, y] == '#' ? 1 : 0;
            count += Map[x, y - 1] == '#' ? 1 : 0;
            count += Map[x, y + 1] == '#' ? 1 : 0;

            while (count > 1)
            {
                var tDx = 0;
                var tDy = 0;
                
                if (Map[x + 1, y] != '#' && ! visited.Contains((x + 1, y)))
                {
                    tDx = 1;
                    tDy = 0;
                }
                
                if (Map[x - 1, y] != '#' && ! visited.Contains((x - 1, y)))
                {
                    tDx = -1;
                    tDy = 0;
                }
                
                if (Map[x, y + 1] != '#' && ! visited.Contains((x, y + 1)))
                {
                    tDx = 0;
                    tDy = 1;
                }
                
                if (Map[x, y - 1] != '#' && ! visited.Contains((x, y - 1)))
                {
                    tDx = 0;
                    tDy = -1;
                }

                if (tDx == 0 && tDy == 0)
                {
                    break;
                }

                dX = tDx;
                dY = tDy;

                x += dX;
                y += dY;

                visited.Add((x, y));

                Console.WriteLine($"{x}, {y}");
                
                count = 0;

                count += Map[x - 1, y] == '#' ? 1 : 0;
                count += Map[x + 1, y] == '#' ? 1 : 0;
                count += Map[x, y - 1] == '#' ? 1 : 0;
                count += Map[x, y + 1] == '#' ? 1 : 0;

                steps++;
            }
            
            Console.WriteLine(count);
            
            Console.WriteLine($"{x}, {y}: {steps}");
        }
    }
}