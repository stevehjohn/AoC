using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2017._02;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var result = Solve();

        return result.ToString();
    }

    private int Solve()
    {
        var sum = 0;

        foreach (var line in Input)
        {
            var cells = line.Split('\t', StringSplitOptions.TrimEntries).Select(int.Parse).ToList();

            for (var outer = 0; outer < cells.Count; outer++)
            {
                for (var inner = outer + 1; inner < cells.Count; inner++)
                {
                    var left = cells[outer];

                    var right = cells[inner];

                    if (left == right)
                    {
                        continue;
                    }

                    if (Math.Max(left, right) % Math.Min(left, right) == 0)
                    {
                        sum += Math.Max(left, right) / Math.Min(left, right);
                    }
                }
            }
        }

        return sum;
    }
}