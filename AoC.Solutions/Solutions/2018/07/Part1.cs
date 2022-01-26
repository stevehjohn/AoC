using System.Text;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2018._07;

[UsedImplicitly]
public class Part1 : Base
{
    private readonly Dictionary<char, List<char>> _steps = new();

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

            foreach (var item in _steps.Where(s => s.Value.Count > 0))
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

    private IEnumerable<char> ParseInput()
    {
        var allSteps = new HashSet<char>();
        
        var allRequires = new HashSet<char>();

        foreach (var line in Input)
        {
            var step = line[36];

            var requires = line[5];

            if (! _steps.TryGetValue(step, out var requirements))
            {
                requirements = new List<char>();

                _steps.Add(step, requirements);
            }
            else
            {
                requirements = _steps[step];
            }

            requirements.Add(requires);

            allSteps.Add(step);

            allRequires.Add(requires);
        }

        return allRequires.Except(allSteps).OrderBy(s => s);
    }
}