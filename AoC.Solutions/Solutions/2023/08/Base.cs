using System.Collections.Concurrent;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2023._08;

public abstract class Base : Solution
{
    public override string Description => "Haunted wasteland";

    private string _steps;
    
    private readonly ConcurrentDictionary<string, (string Left, string Right)> _map = new();

    protected readonly List<string> Starts = new();
    
    protected void ParseInput()
    {
        _steps = Input[0];

        foreach (var line in Input[2..])
        {
            var key = line[..3];

            if (key[2] == 'A')
            {
                Starts.Add(key);
            }

            _map.TryAdd(line[..3], (line[7..10], line[12..15]));
        }
    }

    protected int WalkMap(string node = "AAA")
    {
        var steps = 0;

        var step = 0;
        
        while (true)
        {
            steps++;
            
            node = _steps[step] == 'L' ? _map[node].Left : _map[node].Right;

            if (node[2] == 'Z')
            {
                break;
            }

            step++;

            if (step == _steps.Length)
            {
                step = 0;
            }
        }

        return steps;
    }
}