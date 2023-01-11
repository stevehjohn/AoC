using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2022._20;

public abstract class Base : Solution
{
    public override string Description => "Grove positioning system";

    private int _length;

    private Number[] _initialNumbers;

    private Number[] _numbers;

    protected void ParseInput(long key = 1)
    {
        _length = Input.Length;

        _initialNumbers = new Number[_length];

        _numbers = new Number[_length];

        for (var i = 0; i < _length; i++)
        {
            var number = new Number(long.Parse(Input[i]) * key);

            _initialNumbers[i] = number;

            _numbers[i] = number;
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
        var zero = _numbers.First(n => n.Value == 0);

        var startIndex = 0;

        for (var i = 0; i < _length; i++)
        {
            if (_numbers[i] == zero)
            {
                startIndex = i;

                break;
            }
        }

        var sum = 0L;

        sum += _numbers[(1_000 + startIndex) % _length].Value;

        sum += _numbers[(2_000 + startIndex) % _length].Value;

        sum += _numbers[(3_000 + startIndex) % _length].Value;

        return sum;
    }

    private void DoSwap(int index)
    {
        var number = _initialNumbers[index];

        var oldIndex = 0;

        for (var i = 0; i < _length; i++)
        {
            if (_numbers[i] == number)
            {
                oldIndex = i;

                break;
            }
        }

        var newIndex = (int) ((oldIndex + number.Value) % (_length - 1));

        if (newIndex < 0)
        {
            newIndex += _length - 1;
        }

        if (oldIndex == newIndex)
        {
            return;
        }

        if (oldIndex < newIndex)
        {
            Array.Copy(_numbers, oldIndex + 1, _numbers, oldIndex, newIndex - oldIndex);
        }
        else
        {
            Array.Copy(_numbers, newIndex, _numbers, newIndex + 1, oldIndex - newIndex);
        }

        _numbers[newIndex] = number;
    }
}