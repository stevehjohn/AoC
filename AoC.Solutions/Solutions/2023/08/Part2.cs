using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._08;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();
        
        return WalkMap().ToString();
    }

    private int WalkMap()
    {
        var steps = 0;

        var step = 0;

        var nodes = Map.Where(n => n.Key.EndsWith('Z')).Select(n => n.Key).ToArray();
        
        while (true)
        {
            steps += 1;

            var endCount = 0;
            
            for (var i = 0; i < nodes.Length; i++)
            {
                nodes[i] = Steps[step] == 'L' ? Map[nodes[i]].Left : Map[nodes[i]].Right;

                if (nodes[i].EndsWith('Z'))
                {
                    endCount++;
                }
            }

            if (endCount == nodes.Length)
            {
                break;
            }

            step++;

            if (step == Steps.Length)
            {
                step = 0;
            }
        }

        return steps;
    }
}