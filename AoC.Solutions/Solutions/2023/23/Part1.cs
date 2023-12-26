using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._23;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var result = Solve();
        
        return result.ToString();
    }
    
    private int Solve()
    {
        var queue = new Queue<(int X, int Y, (int Dx, int Dy) Direction, int Steps, HashSet<(int X, int Y)> Visited)>();
        
        queue.Enqueue((1, 1, South, 0, new HashSet<(int X, int Y)>()));

        var stepCounts = new List<int>();
        
        while (queue.TryDequeue(out var position))
        {
            if (position.X == Width - 2 && position.Y == Height - 2)
            {
                stepCounts.Add(position.Steps);

                continue;
            }

            var tile = Map[position.X, position.Y];
            
            if (tile == '>')
            {
                AddNewPosition(queue, position, East);
            
                continue;
            }
            
            if (tile == 'v')
            {
                AddNewPosition(queue, position, South);
            
                continue;
            }

            if (position.Direction != North)
            {
                AddNewPosition(queue, position, South);
            }

            if (position.Direction != East)
            {
                AddNewPosition(queue, position, West);
            }

            if (position.Direction != South)
            {
                AddNewPosition(queue, position, North);
            }

            if (position.Direction != West)
            {
                AddNewPosition(queue, position, East);
            }
        }
        
        return stepCounts.Max();
    }

    private void AddNewPosition(
        Queue<(int X, int Y, (int Dx, int Dy) Direction, int Steps, HashSet<(int X, int Y)>)> queue,
        (int X, int Y, (int Dx, int Dy) Direction, int Steps, HashSet<(int X, int Y)> Visited) position, 
        (int Dx, int Dy) direction)
    {
        if (Map[position.X + direction.Dx, position.Y + direction.Dy] != '#')
        {
            if (position.Visited.Add((position.X + direction.Dx, position.Y + direction.Dy)))
            {
                queue.Enqueue((position.X + direction.Dx, position.Y + direction.Dy, direction, position.Steps + 1,
                    new HashSet<(int X, int Y)>(position.Visited)));
            }
        }
    }
}