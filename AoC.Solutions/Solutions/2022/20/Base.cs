using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2022._20;

public abstract class Base : Solution
{
    public override string Description => "Grove positioning system";

    private int _length;

    private long[] _values;

    private int[] _order;

    private int[] _offsets;

    private int _zero;

    protected void ParseInput(long key = 1)
    {
        _length = Input.Length;

        _values = new long[_length];

        _order = new int[_length];

        _offsets = new int[_length];

        for (var i = 0; i < _length; i++)
        {
            var value = long.Parse(Input[i]) * key;

            _values[i] = value;

            _order[i] = i;

            _offsets[i] = (int) (value % (_length - 1));

            if (value == 0)
            {
                _zero = i;
            }
        }
    }

    protected void MixState(int times = 1)
    {
        for (var t = 0; t < times; t++)
        {
            for (var i = 0; i < _length; i++)
            {
                DoSwap(i);
            }
        }
    }

    protected long Solve()
    {
        var start = Array.IndexOf(_order, _zero);

        return _values[_order[(start + 1_000) % _length]] + _values[_order[(start + 2_000) % _length]] + _values[_order[(start + 3_000) % _length]];
    }

    private void DoSwap(int index)
    {
        var offset = _offsets[index];

        if (offset == 0)
        {
            return;
        }

        var oldIndex = Array.IndexOf(_order, index);

        var newIndex = oldIndex + offset;

        var modulus = _length - 1;

        if (newIndex < 0)
        {
            newIndex += modulus;
        }
        else if (newIndex >= modulus)
        {
            newIndex -= modulus;
        }

        if (oldIndex == newIndex)
        {
            return;
        }

        if (oldIndex < newIndex)
        {
            Array.Copy(_order, oldIndex + 1, _order, oldIndex, newIndex - oldIndex);
        }
        else
        {
            Array.Copy(_order, newIndex, _order, newIndex + 1, oldIndex - newIndex);
        }

        _order[newIndex] = index;
    }
}