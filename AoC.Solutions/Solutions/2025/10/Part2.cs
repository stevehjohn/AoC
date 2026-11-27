using System.Numerics;
using AoC.Solutions.Extensions;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2025._10;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var sum = 0;

        foreach (var line in Input)
        {
            var parts = line.Split(' ');

            var buttons = new int[parts.Length - 2];

            for (var i = 1; i < parts.Length - 1; i++)
            {
                var digits = parts[i][1..^1].Split(',');

                var button = 0;

                foreach (var digit in digits)
                {
                    button |= 1 << int.FastParse(digit);
                }

                buttons[i - 1] = button;
            }

            var joltages = parts[^1][1..^1].Split(',');

            var totals = new int[joltages.Length];

            for (var i = 0; i < totals.Length; i++)
            {
                totals[i] = int.FastParse(joltages[i]);
            }

            sum += Solve(buttons, totals);
        }

        return sum.ToString();
    }

    private static int Solve(int[] buttons, int[] totals)
    {
        var maximum = Math.Max(
            totals.Max(), buttons.Length);

        var bits = BitOperations.Log2(
            (uint) Math.Max(1, maximum)) + 2;

        if (bits * totals.Length > 128 ||
            buttons.Length > 18)
        {
            return new MatrixSolver(new Matrix(buttons, totals)).Solve();
        }

        var target = UInt128.Zero;

        var guard = UInt128.Zero;

        var parity = UInt128.Zero;

        for (var i = 0; i < totals.Length; i++)
        {
            var shift = i * bits;

            target |= (UInt128) totals[i] << shift;

            guard |= UInt128.One << (shift + bits - 1);

            parity |= UInt128.One << shift;
        }

        var packedButtons = new UInt128[buttons.Length];

        for (var i = 0; i < buttons.Length; i++)
        {
            for (var j = 0; j < totals.Length; j++)
            {
                if ((buttons[i] & (1 << j)) != 0)
                {
                    packedButtons[i] |= UInt128.One << (j * bits);
                }
            }
        }

        var groups = new Dictionary<UInt128, List<(UInt128 Delta, int Cost)>>();

        var delta = UInt128.Zero;

        var previous = 0;

        var subsets = 1 << buttons.Length;

        for (var i = 0; i < subsets; i++)
        {
            var gray = i ^ (i >> 1);

            if (i != 0)
            {
                var changed = gray ^ previous;

                var button = BitOperations.TrailingZeroCount((uint) changed);

                if ((gray & changed) != 0)
                {
                    delta += packedButtons[button];
                }
                else
                {
                    delta -= packedButtons[button];
                }
            }

            var key = delta & parity;

            if (! groups.TryGetValue(key, out var group))
            {
                group = [];

                groups.Add(key, group);
            }

            group.Add((delta, BitOperations.PopCount((uint) gray)));

            previous = gray;
        }

        var memo = new Dictionary<UInt128, int>();

        var result = Search(target);

        if (result == int.MaxValue)
        {
            throw new InvalidOperationException("No solution found.");
        }

        return result;

        int Search(UInt128 remaining)
        {
            if (remaining == 0)
            {
                return 0;
            }

            if (memo.TryGetValue(remaining, out var cached))
            {
                return cached;
            }

            var best = int.MaxValue;

            if (groups.TryGetValue(remaining & parity, out var candidates))
            {
                foreach (var (contribution, cost) in candidates)
                {
                    if (cost >= best)
                    {
                        continue;
                    }

                    var difference = (remaining | guard) - contribution;

                    if ((difference & guard) != guard)
                    {
                        continue;
                    }

                    var next = (remaining - contribution) >> 1;

                    var subResult = Search(next);

                    if (subResult == int.MaxValue)
                    {
                        continue;
                    }

                    best = Math.Min(best, cost + 2 * subResult);
                }
            }

            memo.Add(remaining, best);

            return best;
        }
    }
}