using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2020._07;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var queue = new Queue<(string Container, long Multiplier)>();

        queue.Enqueue(("shiny gold", 1));

        var containedCount = 0L;

        while (queue.Count > 0)
        {
            var container = queue.Dequeue();

            var containerContains = BagData.Where(b => b.Container == container.Container).ToList();

            foreach (var bag in containerContains)
            {
                var bagCount = container.Multiplier * bag.Count;

                containedCount += bagCount;

                queue.Enqueue((bag.Contains, bagCount));
            }
        }

        return containedCount.ToString();
    }
}