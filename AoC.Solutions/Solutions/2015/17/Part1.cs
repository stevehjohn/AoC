using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2015._17;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var containers = Input.Select((v, i) => (Index: i, Value: int.Parse(v))).ToDictionary(i => 1 << i.Index, i => i.Value);

        var combinations = 1 << containers.Count;

        var count = 0;

        for (var i = 0; i < combinations; i++)
        {
            var total = containers.Where(c => (c.Key & i) > 0).Sum(c => c.Value);

            if (total == 150)
            {
                count++;
            }
        }

        return count.ToString();
    }
}