using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2017._15;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var generatorA = long.Parse(Input[0].Split(' ')[^1]);

        var generatorB = long.Parse(Input[1].Split(' ')[^1]);

        var matches = 0;

        var queueA = new Queue<long>();

        var queueB = new Queue<long>();

        while (queueA.Count < 5_000_000)
        {
            generatorA = generatorA * 16807 % int.MaxValue;

            if (generatorA % 4 == 0)
            {
                queueA.Enqueue(generatorA);
            }
        }

        while (queueB.Count < 5_000_000)
        {
            generatorB = generatorB * 48271 % int.MaxValue;

            if (generatorB % 8 == 0)
            {
                queueB.Enqueue(generatorB);
            }
        }

        while (queueA.Count > 0)
        {
            if ((queueA.Dequeue() & 65_535) == (queueB.Dequeue() & 65_535))
            {
                matches++;
            }
        }

        return matches.ToString();
    }
}