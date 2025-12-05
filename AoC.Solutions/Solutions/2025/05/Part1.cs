using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2025._05;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var ranges = new List<(long Start, long End)>();

        var index = 0;
        
        foreach (var line in Input)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                break;
            }

            var parts = line.Split('-');
            
            ranges.Add((long.Parse(parts[0]), long.Parse(parts[1])));

            index++;
        }

        index += 2;

        var fresh = 0;
        
        for (; index < Input.Length; index++)
        {
            var ingredient = long.Parse(Input[index]);
            
            for (var i = 0; i < ranges.Count; i++)
            {
                var range = ranges[i];

                if (ingredient >= range.Start && ingredient <= range.End)
                {
                    fresh++;
                    
                    break;
                }
            }
        }

        return fresh.ToString();
    }
}