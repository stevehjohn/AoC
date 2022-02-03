using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2018._15;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var i = 4;

        (bool NoElfLosses, int Outcome) result;

        while (true)
        {
            Reset();

            ParseInput();

            result = Play(i);

            if (result.NoElfLosses)
            {
                break;
            }

            i++;
        }

        return result.Outcome.ToString();
    }
}