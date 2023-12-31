using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2018._05;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var smallest = int.MaxValue;

        for (var c = 'a'; c <= 'z'; c++)
        {
            var result = ReactPolymer(Input[0].Replace(c.ToString(), string.Empty, StringComparison.InvariantCultureIgnoreCase));

            if (result < smallest)
            {
                smallest = result;
            }
        }

        return smallest.ToString();
    }
}