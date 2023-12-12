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
        if (_cache.TryGetValue(row, out var answer))
        {
            return answer;
        }

        answer = CalculateArrangements(row, groups);
        
        _cache.Add(row, answer);

        return answer;
    }

    private long CalculateArrangements(string row, int[] groups)
    {
        row = row.TrimStart('.');

        var length = row.Length;
        
        if (length == 0)
        {
            return groups.Length == 0 ? 1 : 0;
        }

        if (groups.Length == 0)
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
                return groups.Length == 1 ? 1 : 0;
            }

            if (row[group] == '#')
            {
                return 0;
            }
            
            return GetArrangements(row[(group + 1)..], groups[1..]);
        }

        return GetArrangements($"#{row[1..]}", groups) + GetArrangements(row[1..], groups);
    }
}