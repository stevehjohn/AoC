using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2025._12;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var areas = new Dictionary<int, int>();

        var i = 0;
        
        for (;; i += 5)
        {
            if (Input[i][1] != ':')
            {
                break;
            }

            var area = 0;
            
            for (var y = 1; y < 4; y++)
            {
                area += Input[i + y].Count(c => c == '#');
            }
            
            areas.Add(i / 5, area);
        }

        var result = 0;

        for (; i < Input.Length; i++)
        {
            var line = Input[i];

            var area = int.Parse(line[..2]) * int.Parse(line[3..5]);

            var presentCounts = line[7..].Split(' ');

            var used = 0;

            for (var x = 0; x < presentCounts.Length; x++)
            {
                var required = int.Parse(presentCounts[x]);

                used += required * areas[x];
            }

            if (used < area)
            {
                result++;
            }
        }

        return result.ToString();
    }
}
