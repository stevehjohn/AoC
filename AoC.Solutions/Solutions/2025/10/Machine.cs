using AoC.Solutions.Extensions;

namespace AoC.Solutions.Solutions._2025._10;

public class Machine
{
    private readonly int _display;

    private readonly int[] _buttons;
    
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
}