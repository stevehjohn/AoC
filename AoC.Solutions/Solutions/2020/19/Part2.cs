using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2020._19;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput(true);

        var validCount = 0;

        foreach (var message in Messages)
        {
            validCount += CheckMessage(message) ? 1 : 0;
        }

        return validCount.ToString();
    }

    private bool CheckMessage(string message)
    {
        var matchRuns42 = CheckRule(Rules[42], message);

        var matchRuns31 = CheckRule(Rules[31], message);

        return matchRuns42.RunCount == 1 && matchRuns31.RunCount == 1 && matchRuns42.MatchCount > matchRuns31.MatchCount;
    }

    private static (int RunCount, int MatchCount) CheckRule(Rule rule, string data)
    {
        var runCount = 0;

        var previousValid = false;

        var matchCount = 0;

        while (true)
        {
            var result = rule.Validate(data);

            matchCount += result.IsValid ? 1 : 0;

            if (result.IsValid && ! previousValid)
            {
                runCount++;
            }

            previousValid = result.IsValid;

            data = data[8..];

            if (data.Length == 0)
            {
                break;
            }
        }

        return (runCount, matchCount);
    }
}