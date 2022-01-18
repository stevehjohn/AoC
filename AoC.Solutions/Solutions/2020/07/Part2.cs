using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2020._07;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var goldBagsInBags = BagData.Where(b => b.Contains == "shiny gold").ToList();

        var queue = new PriorityQueue<(string Container, int level), int>();

        goldBagsInBags.ForEach(b => queue.Enqueue((b.Container, 0), int.MaxValue));

        var topLevelBags = new HashSet<string>();

        while (queue.Count > 0)
        {
            var container = queue.Dequeue();

            Console.WriteLine($"{new string(' ', container.level * 2)}{container.Container}");

            var containerContainedIn = BagData.Where(b => b.Contains == container.Container).ToList();

            topLevelBags.Add(container.Container);

            if (containerContainedIn.Count == 0)
            {
                continue;
            }

            containerContainedIn.ForEach(b => queue.Enqueue((b.Container, container.level + 1), int.MaxValue - (container.level + 1)));
        }

        return topLevelBags.Count.ToString();
    }
}