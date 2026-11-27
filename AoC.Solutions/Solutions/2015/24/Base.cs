using System.Numerics;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2015._24;

public abstract class Base : Solution
{
    public override string Description => "It hangs in the balance";

    protected long Smallest = long.MaxValue;

    private int[] _values;

    private int[] _prefix;

    private int _count;

    private int _target;

    private int _groups;

    private ulong _all;

    private readonly Dictionary<ulong, bool> _partitionCache = [];

    protected void TryFindQeOfGroup(List<int> weights, int target)
    {
        _values = [.. weights.OrderByDescending(w => w)];

        _count = _values.Length;

        _target = target;

        if (_count > 64)
        {
            throw new InvalidOperationException("Too many packages.");
        }

        _all = _count == 64 ? ulong.MaxValue : (1UL << _count) - 1;

        _groups = _values.Sum() / _target;

        _prefix = new int[_count + 1];

        for (var i = 0; i < _count; i++)
        {
            _prefix[i + 1] = _prefix[i] + _values[i];
        }

        _partitionCache.Clear();

        Smallest = long.MaxValue;

        for (var size = 1; size <= _count; size++)
        {
            Search(0, size, _target, 1, 0);

            if (Smallest != long.MaxValue)
            {
                return;
            }
        }
    }

    private void Search(int start, int left, int remaining, long product, ulong used)
    {
        if (left == 0)
        {
            if (remaining == 0 && product < Smallest && CanPartition(_all ^ used, _groups - 1))
            {
                Smallest = product;
            }

            return;
        }

        if (_count - start < left || remaining <= 0 || product >= Smallest)
        {
            return;
        }

        var maximum = _prefix[start + left] - _prefix[start];

        var minimum = _prefix[_count] - _prefix[_count - left];

        if (remaining > maximum || remaining < minimum)
        {
            return;
        }

        for (var i = start; i <= _count - left; i++)
        {
            var value = _values[i];

            if (value > remaining || product > long.MaxValue / value)
            {
                continue;
            }

            Search(i + 1, left - 1, remaining - value, product * value, used | (1UL << i));
        }
    }

    private bool CanPartition(ulong available, int remainingGroups)
    {
        if (remainingGroups == 1)
        {
            return true;
        }

        if (_partitionCache.TryGetValue(available, out var cached))
        {
            return cached;
        }

        var first = BitOperations.TrailingZeroCount(available);

        var rest = available & ~(1UL << first);

        var result = FindGroup(rest, rest, _target - _values[first], remainingGroups);

        _partitionCache[available] = result;

        return result;
    }

    private bool FindGroup(ulong remaining, ulong candidates, int needed, int remainingGroups)
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

            var value = _values[index];

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