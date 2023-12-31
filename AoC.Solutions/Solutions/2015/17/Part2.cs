using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2015._17;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var containers = Input.Select((v, i) => (Index: i, Value: int.Parse(v))).ToDictionary(i => 1 << i.Index, i => i.Value);

        var combinations = 1 << containers.Count;

        var minContainers = int.MaxValue;

        for (var i = 0; i < combinations; i++)
        {
            var iteration = containers.Where(c => (c.Key & i) > 0).ToList();

            if (iteration.Sum(c => c.Value) == 150 && iteration.Count < minContainers)
            {
                minContainers = iteration.Count;
            }
        }

        var count = 0;

        for (var i = 0; i < combinations; i++)
        {
            var iteration = containers.Where(c => (c.Key & i) > 0).ToList();

            if (iteration.Sum(c => c.Value) == 150 && iteration.Count == minContainers)
            {
                count++;
            }
        }

        return count.ToString();
    }
}