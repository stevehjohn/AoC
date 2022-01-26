using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2018._04;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        Data = ParseInput();

        var guard = GetSleepiestGuard();

        var mostAsleep = GetMinuteMostAsleep(guard);

        return (guard * mostAsleep.Minute).ToString();
    }
}
