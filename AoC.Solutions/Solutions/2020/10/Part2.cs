using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2020._10;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseData();

        var differences = GetDifferences();

        var groups = string.Concat(differences.Select(d => (char) ('0' + d))).Split('3', StringSplitOptions.RemoveEmptyEntries).Where(g => g.Length > 1);

        var groupCounts = groups.GroupBy(g => g).Select(g => (g.Key.Length, Count: g.Count())).OrderBy(g => g.Length);

        var result = 1L;

        var tribonacciHistory = new List<int> { 0, 1, 1 };

        var targetLength = 2;

        foreach (var group in groupCounts)
        {
            var length = group.Length;

            while (length <= targetLength)
            {
                tribonacciHistory.Add(tribonacciHistory.GetRange(tribonacciHistory.Count - 3, 3).Sum());

                length++;
            }

            targetLength++;

            result *= (long) Math.Pow(tribonacciHistory.Last(), group.Count);
        }

        return result.ToString();
    }
}