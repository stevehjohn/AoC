using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2021._03;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var oxygen = Filter(Input.ToList());

        var co2 = Filter(Input.ToList(), true);

        return (Convert.ToInt32(oxygen, 2) * Convert.ToInt32(co2, 2)).ToString();
    }

    private static string Filter(List<string> items, bool leastCommon = false)
    {
        var width = items[0].Length;

        for (var i = 0; i < width; i++)
        {
            var ones = items.Count(item => item[i] == '1');

            var bitToKeep = leastCommon
                ? ones >= (float) items.Count / 2
                    ? '0'
                    : '1'
                : ones >= (float) items.Count / 2
                    ? '1'
                    : '0';

            items = items.Where(item => item[i] == bitToKeep).ToList();

            if (items.Count == 1)
            {
                break;
            }
        }

        return items.Single();
    }
}