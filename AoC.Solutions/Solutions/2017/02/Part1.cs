using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2017._02;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var result = GetChecksum();

        return result.ToString();
    }

    private int GetChecksum()
    {
        var sum = 0;

        foreach (var line in Input)
        {
            var cells = line.Split('\t', StringSplitOptions.TrimEntries).Select(int.Parse).ToList();

            sum += cells.Max() - cells.Min();
        }

        return sum;
    }
}