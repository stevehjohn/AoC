using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2025._05;

public abstract class Base : Solution
{
    public override string Description => "Cafeteria";

    protected readonly List<Infrastructure.Range<long>> Ranges = [];

    protected int ParseRanges()
    {
        var index = 0;
        
        foreach (var line in Input)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                break;
            }

            var parts = line.Split('-');
            
            Ranges.Add(new Range<long>(long.Parse(parts[0]), long.Parse(parts[1])));

            index++;
        }

        return index;
    }
}