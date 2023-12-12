using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2023._12;

public abstract class Base : Solution
{
    public override string Description => "Hot springs";

    private readonly Dictionary<string, long> _cache = new();
    
    protected static (string Row, int[] Groups) ParseLine(string line)
    {
        var parts = line.Split(' ');

        var row = parts[0].Trim('.');

        var groups = parts[1].Split(',').Select(int.Parse).ToArray();

        return (row, groups);
    }

    protected long GetArrangements(string row, int[] groups)
    {
        var key = $"{row}{string.Join(',', groups)}";
        
        if (_cache.TryGetValue(key, out var answer))
        {
            return answer;
        }

        answer = CalculateArrangements(row, groups);
        
        _cache.Add(key, answer);

        return answer;
    }

    private static long CalculateArrangements(string row, int[] groups)
    {
        row = row.TrimStart('.');

        var length = row.Length;
        
        var groupLength = groups.Length;
        
        if (length == 0)
        {
            return groupLength == 0 ? 1 : 0;
        }

        if (groupLength == 0)
        {
            return row.Contains('#') ? 0 : 1;
        }

        if (row[0] == '#')
        {
            var group = groups[0];

            if (length < group || row[..group].Contains('.'))
            {
                return 0;
            }

            if (length == group)
            {
                return groupLength == 1 ? 1 : 0;
            }

            if (row[group] == '#')
            {
                return 0;
            }
            
            return CalculateArrangements(row[(group + 1)..], groups[1..]);
        }

        return CalculateArrangements($"#{row[1..]}", groups) + CalculateArrangements(row[1..], groups);
    }
}