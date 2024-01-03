using System.Text;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2018._07;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var startSteps = ParseInput();

        var result = Solve(startSteps);

        return result;
    }

    private string Solve(IEnumerable<char> startStep)
    {
        var result = new StringBuilder();

        var queue = new PriorityQueue<char, char>();

        foreach (var item in startStep)
        {
            queue.Enqueue(item, item);
        }

        while (queue.Count > 0)
        {
            var step = queue.Dequeue();
            
            result.Append(step);

            foreach (var item in Steps.Where(s => s.Value.Count > 0))
            {
                item.Value.Remove(step);

                if (item.Value.Count == 0)
                {
                    queue.Enqueue(item.Key, item.Key);
                }
            }
        }

        return result.ToString();
    }
}