using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._08;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var result = WalkMap();
        
        return result.ToString();
    }

    private int WalkMap()
    {
        var steps = 0;

        var step = 0;

        var node = "AAA";
        
        while (true)
        {
            steps++;
            
            node = Steps[step] == 'L' ? Map[node].Left : Map[node].Right;

            if (node == "ZZZ")
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