using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2024._21;

public abstract class Base : Solution
{
    public override string Description => "Keypad conundrum";

    private readonly Dictionary<char, Dictionary<char, char>> _numberPadMoves =
        new()
        {
            { '0', new Dictionary<char, char> { { '2', '^' }, { 'A', '>' } } },
            { '1', new Dictionary<char, char> { { '2', '>' }, { '4', '^' } } },
            { '2', new Dictionary<char, char> { { '0', 'v' }, { '1', '<' }, { '3', '>' }, { '5', '^' } } },
            { '3', new Dictionary<char, char> { { '2', '<' }, { '6', '^' }, { 'A', 'v' } } },
            { '4', new Dictionary<char, char> { { '1', 'v' }, { '5', '>' }, { '7', '^' } } },
            { '5', new Dictionary<char, char> { { '2', 'v' }, { '4', '<' }, { '6', '>' }, { '8', '^' } } },
            { '6', new Dictionary<char, char> { { '3', 'v' }, { '5', '<' }, { '9', '^' } } },
            { '7', new Dictionary<char, char> { { '4', 'v' }, { '8', '>' } } },
            { '8', new Dictionary<char, char> { { '5', 'v' }, { '7', '<' }, { '9', '>' } } },
            { '9', new Dictionary<char, char> { { '6', 'v' }, { '8', '<' } } },
            { 'A', new Dictionary<char, char> { { '0', '<' }, { '3', '^' } } }
        };

    private readonly Dictionary<char, Dictionary<char, char>> _dPadMoves =
        new()
        {
            { '^', new Dictionary<char, char> { { 'A', '>' }, { 'v', 'v' } } },
            { '<', new Dictionary<char, char> { { 'v', '>' } } },
            { 'v', new Dictionary<char, char> { { '<', '<' }, { '^', '^' }, { '>', '>' } } },
            { '>', new Dictionary<char, char> { { 'v', '<' }, { 'A', '^' } } },
            { 'A', new Dictionary<char, char> { { '^', '<' }, { '>', 'v' } } }
        };
    
    private readonly Dictionary<(string, int, bool), long> _cache = [];
    
    protected long Solve(string code, int depth, Dictionary<char, Dictionary<char, char>> pad = null)
    {
        if (pad == null)
        {
            pad = _numberPadMoves;
        }

        if (_cache.TryGetValue((code, depth, pad == _dPadMoves), out var result))
        {
            return result;
        }

        code = $"A{code}";

        result = 0L;
        
        for (var i = 0; i < code.Length - 1; i++)
        {
            var paths = GetPaths(code[i], code[i + 1], pad);

            if (depth == 0)
            {
                result += paths.MinBy(p => p.Length).Length;
            }
            else
            {
                var minimum = long.MaxValue;
                
                foreach (var path in paths)
                {
                    var next = Solve(path, depth - 1, _dPadMoves);

                    if (next < minimum)
                    {
                        minimum = next;
                    }
                }

                result += minimum;
            }
        }

        _cache[(code, depth, pad == _dPadMoves)] = result;

        return result;
    }

    private static List<string> GetPaths(char start, char end, Dictionary<char, Dictionary<char, char>> pad)
    {
        var queue = new PriorityQueue<(char Position, HashSet<char> Visited, string Path), int>();
        
        queue.Enqueue((start, [], string.Empty), 0);

        var paths = new List<string>();

        var minLength = int.MaxValue;
        
        while (queue.Count > 0)
        {
            var state = queue.Dequeue();

            if (state.Position == end)
            {
                if (state.Path.Length <= minLength)
                {
                    minLength = state.Path.Length;
                }
                else
                {
                    break;
                }

                paths.Add($"{state.Path}A");
            }

            foreach (var key in pad[state.Position])
            {
                if (! state.Visited.Contains(key.Key))
                {
                    queue.Enqueue((key.Key, [..state.Visited, key.Key], $"{state.Path}{key.Value}"), state.Path.Length + 1);
                }
            }
        }

        return paths;
    }
}