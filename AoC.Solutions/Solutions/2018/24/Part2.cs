using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2018._24;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        // Could do a binary thing here, but it runs fast enough.
        var boost = 1;

        while (true)
        {
            ParseInput();

            var result = Play(boost);

            if (result.Side == Type.ImmuneSystem)
            {
                return result.Units.ToString();
            }

            boost++;
        }
    }
}