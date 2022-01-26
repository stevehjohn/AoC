using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2018._04;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        Data = ParseInput();

        var guardIds = Data.Select(d => d.GuardId).Distinct();

        var minuteMostAsleep = new List<(int GuardId, int Minute, int Times)>();

        foreach (var guardId in guardIds)
        {
            var mostAsleep = GetMinuteMostAsleep(guardId);

            minuteMostAsleep.Add((guardId, mostAsleep.Minute, mostAsleep.Times));
        }

        var result = minuteMostAsleep.MaxBy(d => d.Times);

        return (result.GuardId * result.Minute).ToString();
    }
}
