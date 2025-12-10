namespace AoC.Solutions.Solutions._2025._10;

public class Machine
{
    private readonly int _display;

    private readonly int[] _buttons;

    private readonly int[] _joltages;

    public Machine(string configuration)
    {
        var parts = configuration.Split(' ');

        for (var i = 1; i < parts[0].Length; i++)
        {
            if (parts[0][i] == '#')
            {
                _display |= 1 << (i - 1);
            }
        }

        _buttons = new int[parts.Length - 2];

        for (var i = 1; i < parts.Length - 1; i++)
        {
            var digits = parts[i][1..^1].Split(',');

            var button = 0;

            for (var d = 0; d < digits.Length; d++)
            {
                button |= 1 << (digits[d][0] - '0');
            }

            _buttons[i - 1] = button;
        }

        var joltages = parts[^1][1..^1].Split(',');

        _joltages = new int[joltages.Length];

        for (var i = 0; i < joltages.Length; i++)
        {
            _joltages[i] = int.Parse(joltages[i]);
        }
    }

    public int SwitchOn()
    {
        var queue = new Queue<(int state, int presses)>();

        var visited = new HashSet<int>();

        queue.Enqueue((0, 0));

        visited.Add(0);

        while (queue.Count > 0)
        {
            var (state, presses) = queue.Dequeue();

            if (state == _display)
            {
                return presses;
            }

            foreach (var button in _buttons)
            {
                var newState = state ^ button;

                if (visited.Add(newState))
                {
                    queue.Enqueue((newState, presses + 1));
                }
            }
        }

        return 0;
    }

    public int ConfigureJoltage()
    {
        var queue = new PriorityQueue<(int[] counters, int sum), int>();
    
        var visited = new HashSet<string>();
    
        var initial = new int[_buttons.Length];
        
        queue.Enqueue((initial, 0), 0);
        
        visited.Add(string.Join(",", initial));

        var max = _joltages.Sum();
    
        while (queue.Count > 0)
        {
            var (presses, totalPresses) = queue.Dequeue();

            // Calculate current counter state
            var counters = new int[_joltages.Length];
            
            for (var b = 0; b < _buttons.Length; b++)
            {
                for (var i = 0; i < _joltages.Length; i++)
                {
                    if ((_buttons[b] & (1 << i)) != 0)
                    {
                        counters[i] += presses[b];
                    }
                }
            }
    
            // Check if we've reached target
            var sum = 0;

            var valid = true;
            
            for (var i = 0; i < _joltages.Length; i++)
            {
                sum += counters[i];

                if (sum > max)
                {
                    break;
                }

                if (counters[i] != _joltages[i])
                {
                    valid = false;
                }
            }

            if (valid)
            {
                return totalPresses;
            }

            // Try pressing each button one more time
            for (var b = 0; b < _buttons.Length; b++)
            {
                valid = true;
                var newPresses = (int[]) presses.Clone();
                newPresses[b]++;

                if (totalPresses + 1 > max)
                {
                    valid = false;
                    break;
                }

                // Check if this would exceed any target
                for (var i = 0; i < _joltages.Length; i++)
                {
                    if ((_buttons[b] & (1 << i)) != 0)
                    {
                        if (counters[i] + 1 > _joltages[i])
                        {
                            valid = false;
                            break;
                        }
                    }
                }
    
                if (valid)
                {
                    var key = string.Join(",", newPresses);
                    if (visited.Add(key))
                    {
                        var newTotal = totalPresses + 1;
                        // Priority based on distance to target
                        var remaining = 0;
                        for (var i = 0; i < _joltages.Length; i++)
                        {
                            var counterValue = counters[i];
                            if ((_buttons[b] & (1 << i)) != 0)
                            {
                                counterValue++;
                            }
    
                            remaining += Math.Max(0, _joltages[i] - counterValue);
                        }

                        if (newTotal <= max)
                        {
                            queue.Enqueue((newPresses, newTotal), newTotal + remaining);
                        }
                    }
                }
            }
        }
    
        return 0;
    }
}