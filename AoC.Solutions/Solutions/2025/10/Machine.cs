using System.Numerics;

namespace AoC.Solutions.Solutions._2025._10;

public class Machine
{
    private readonly int _display;

    private readonly int[] _buttons;

    private readonly int[] _joltages;

    private readonly int _buttonCount;

    private readonly int _joltageCount;

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

        _buttonCount = _buttons.Length;

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

        _joltageCount = _joltages.Length;

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

    public int ConfigureJoltageDfs()
    {
        // Remaining required joltages per digit
        var need = (int[]) _joltages.Clone();

        // All buttons: indices 0.._buttonCount-1
        var availableButtons = Enumerable.Range(0, _buttonCount).ToList();

        // Precompute which buttons affect which digit
        var digitToButtons = new List<int>[_joltageCount];

        for (var d = 0; d < _joltageCount; d++)
        {
            digitToButtons[d] = [];
        }

        for (var b = 0; b < _buttonCount; b++)
        {
            var mask = _buttons[b];

            while (mask > 0)
            {
                var digit = BitOperations.TrailingZeroCount((uint) mask);

                if (digit < _joltageCount)
                {
                    digitToButtons[digit].Add(b);
                }

                mask &= mask - 1;
            }
        }

        var result = DfsSolve(need, availableButtons, digitToButtons);

        return result == int.MaxValue ? 0 : result;
    }

    // depth-first search: returns minimal extra presses needed, or int.MaxValue if impossible
    private int DfsSolve(int[] need, List<int> availableButtons, List<int>[] digitToButtons)
    {
        // Check if we are done
        var remainingDigit = -1;

        for (var d = 0; d < need.Length; d++)
        {
            switch (need[d])
            {
                case < 0:
                    return int.MaxValue; // overshot
                
                case > 0:
                    remainingDigit = d;
                    break;
            }
        }

        if (remainingDigit == -1)
        {
            return 0; // all joltages satisfied
        }

        // Pick the digit with >0 need and the fewest buttons (MRV heuristic)
        var bestDigit = -1;
        var bestButtonCount = int.MaxValue;

        for (var d = 0; d < need.Length; d++)
        {
            if (need[d] <= 0)
            {
                continue;
            }

            // count available buttons affecting d
            var count = 0;

            foreach (var b in digitToButtons[d])
            {
                if (availableButtons.Contains(b))
                {
                    count++;
                }
            }

            if (count == 0)
            {
                return int.MaxValue;
            }

            if (count < bestButtonCount)
            {
                bestButtonCount = count;
                bestDigit = d;
            }
        }


        var targetDigit = bestDigit;
        var affecting = new List<int>();

        foreach (var b in digitToButtons[targetDigit])
        {
            if (availableButtons.Contains(b))
            {
                affecting.Add(b);
            }
        }

        var k = affecting.Count;
        var needForDigit = need[targetDigit];

        // enumerate all compositions of needForDigit into k non-negative integers
        var pressesForButtons = new int[k];
        var best = int.MaxValue;

        RecursePartitions(0, needForDigit);

        return best;

        void TryAssignment()
        {
            // Apply this (c0..ck-1) to a copy of need
            var newNeed = (int[]) need.Clone();

            for (var i = 0; i < k; i++)
            {
                var bIndex = affecting[i];
                var presses = pressesForButtons[i];

                if (presses == 0)
                {
                    continue;
                }

                var mask = _buttons[bIndex];

                while (mask > 0)
                {
                    var d = BitOperations.TrailingZeroCount((uint) mask);

                    if (d < newNeed.Length)
                    {
                        newNeed[d] -= presses;
                        if (newNeed[d] < 0)
                        {
                            // overshoot; abandon this assignment early
                            return;
                        }
                    }

                    mask &= mask - 1;
                }
            }

            // Remove the digit and the buttons we just “used” from further recursion
            var newAvailableButtons = new List<int>(availableButtons);

            foreach (var bIndex in affecting)
            {
                newAvailableButtons.Remove(bIndex);
            }

            // targetDigit's need should now be 0, but we don't need to special-case it;
            // DfsSolve will see its need <= 0 and skip it.

            var extra = pressesForButtons.Sum();

            var sub = DfsSolve(newNeed, newAvailableButtons, digitToButtons);

            if (sub == int.MaxValue)
            {
                return;
            }

            var total = extra + sub;

            if (total < best)
            {
                best = total;
            }
        }

        void RecursePartitions(int idx, int remaining)
        {
            if (idx == k - 1)
            {
                pressesForButtons[idx] = remaining;

                TryAssignment();
                return;
            }

            // slightly prunable: you can bound the loop using max useful presses, but simple is fine
            for (var v = 0; v <= remaining; v++)
            {
                pressesForButtons[idx] = v;
                RecursePartitions(idx + 1, remaining - v);
            }
        }
    }
}