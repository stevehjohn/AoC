using System.Numerics;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2015._24;

public abstract class Base : Solution
{
    public override string Description => "It hangs in the balance";

    protected long Smallest = long.MaxValue;

    protected void TryFindQeOfGroup(List<int> weights, int target)
    {
        var values = weights.OrderByDescending(w => w).ToArray();

        var n = values.Length;

        if (n > 64)
        {
            throw new InvalidOperationException("Too many packages.");
        }

        var all = n == 64 ? ulong.MaxValue : (1UL << n) - 1;

        var groups = values.Sum() / target;

        var prefix = new int[n + 1];

        for (var i = 0; i < n; i++)
        {
            prefix[i + 1] = prefix[i] + values[i];
        }

        var partitionCache = new Dictionary<ulong, bool>();

        Smallest = long.MaxValue;

        for (var size = 1; size <= n; size++)
        {
            Search(0, size, target, 1, 0);

            if (Smallest != long.MaxValue)
            {
                return;
            }
        }

        void Search(int start, int left, int remaining, long product, ulong used)
        {
            if (left == 0)
            {
                if (remaining == 0 && product < Smallest && CanPartition(all ^ used, groups - 1))
                {
                    Smallest = product;
                }

                return;
            }

            if (n - start < left || remaining <= 0 || product >= Smallest)
            {
                return;
            }

            var maximum = prefix[start + left] - prefix[start];

            var minimum = prefix[n] - prefix[n - left];

            if (remaining > maximum || remaining < minimum)
            {
                return;
            }

            for (var i = start; i <= n - left; i++)
            {
                var value = values[i];

                if (value > remaining || product > long.MaxValue / value)
                {
                    continue;
                }

                Search(i + 1, left - 1, remaining - value, product * value, used | (1UL << i));
            }
        }

        bool CanPartition(ulong available, int remainingGroups)
        {
            if (remainingGroups == 1)
            {
                return true;
            }

            if (partitionCache.TryGetValue(available, out var cached))
            {
                return cached;
            }

            var first = BitOperations.TrailingZeroCount(available);

            var rest = available & ~(1UL << first);

            var result = FindGroup(rest, rest, target - values[first], remainingGroups);

            partitionCache[available] = result;

            return result;
        }

        bool FindGroup(ulong remaining, ulong candidates, int needed, int remainingGroups)
        {
            if (needed == 0)
            {
                return CanPartition(remaining, remainingGroups - 1);
            }

            while (candidates != 0)
            {
                var index = BitOperations.TrailingZeroCount(candidates);

                var bit = 1UL << index;

                candidates &= candidates - 1;

                var value = values[index];

                if (value > needed)
                {
                    continue;
                }

                if (FindGroup(remaining ^ bit, candidates, needed - value, remainingGroups))
                {
                    return true;
                }
            }

            return false;
        }
    }
}