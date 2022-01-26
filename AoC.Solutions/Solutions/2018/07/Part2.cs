using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2018._07;

[UsedImplicitly]
public class Part2 : Base
{
    private const int WorkerCount = 5;

    private const int SecondsOffset = 60;

    public override string GetAnswer()
    {
        var startSteps = ParseInput();

        var result = Solve(startSteps);

        return result.ToString();
    }

    private int Solve(IEnumerable<char> startStep)
    {
        var queue = new PriorityQueue<char, char>();

        foreach (var item in startStep)
        {
            queue.Enqueue(item, item);
        }

        var workers = new Dictionary<char, int>();

        var second = -1;

        while (queue.Count > 0 || workers.Count > 0)
        {
            foreach (var worker in workers)
            {
                workers[worker.Key]--;
            }

            var completed = workers.Where(w => w.Value == 0).OrderBy(w => w.Key).ToList();

            completed.ForEach(c => workers.Remove(c.Key));

            foreach (var step in completed)
            {
                foreach (var item in Steps.Where(s => s.Value.Count > 0))
                {
                    item.Value.Remove(step.Key);

                    if (item.Value.Count == 0)
                    {
                        queue.Enqueue(item.Key, item.Key);
                    }
                }
            }

            while (workers.Count < WorkerCount && queue.Count > 0)
            {
                var step = queue.Dequeue();

                workers.Add(step, SecondsOffset + (step - '@'));
            }

            second++;
        }

        return second ;
    }
}