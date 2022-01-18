using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2020._07;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var goldBagsInBags = BagData.Where(b => b.Contains == "shiny gold").ToList();

        var queue = new Queue<string>();

        goldBagsInBags.ForEach(b => queue.Enqueue(b.Container));

        var topLevelBags = new HashSet<string>();

        while (queue.Count > 0)
        {
            var container = queue.Dequeue();

            var containerContainedIn = BagData.Where(b => b.Contains == container).ToList();

            topLevelBags.Add(container);

            if (containerContainedIn.Count == 0)
            {
                continue;
            }

            containerContainedIn.ForEach(b => queue.Enqueue(b.Container));
        }

        return topLevelBags.Count.ToString();
    }
}