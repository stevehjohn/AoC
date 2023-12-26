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
        var queue = new Queue<(int X, int Y)>();

        var visited = new HashSet<(int X, int Y)>();
        
        queue.Enqueue((1, 0));

        while (queue.TryDequeue(out var position))
        {
            if (! visited.Add(position))
            {
                continue;
            }
            
            var count = 
        }
    }
}