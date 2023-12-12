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

    private static long CalculateArrangements(string row, int[] groups)
    {
        row = row.TrimStart('.');

        if (row == string.Empty)
        {
            return groups.Length == 0 ? 1 : 0;
        }

        if (groups.Length == 0)
        {
            return row.IndexOf('#') == -1 ? 1 : 0;
        }

        if (row[0] == '#')
        {
            if (row.Length < groups[0] || row[..groups[0]].Contains('.'))
            {
                return 0;
            }
            
            if (row.Length == groups[0])
            {
                return groups.Length == 1 ? 1 : 0;
            }
            
            if (row[groups[0]] == '#')
            {
                return 0;
            }
            
            return CalculateArrangements(row[(groups[0] + 1)..], groups[1..]);
        }
        
        return CalculateArrangements($"#{row[1..]}", groups) + CalculateArrangements(row[1..], groups);
    }
}