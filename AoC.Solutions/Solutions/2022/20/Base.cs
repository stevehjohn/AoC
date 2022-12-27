using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2022._20;

public abstract class Base : Solution
{
    public override string Description => "Grove positioning system";

    private readonly List<(long Number, int InitialPosition)> _initialNumbers = new();

    private readonly List<(long Number, int InitialPosition)> _numbers = new();

    protected void ParseInput(long key = 1)
    {
        var length = Input.Length;

        for (var i = 0; i < length; i++)
        {
            var number = long.Parse(Input[i]) * key;

            _initialNumbers.Add((number, i));

            _numbers.Add((number, i));
        }
    }

    protected void MixState(int times = 1)
    {
        for (var t = 0; t < times; t++)
        {
            for (var i = 0; i < _numbers.Count; i++)
            {
                DoSwap(i);
            }
        }
    }

    protected long Solve()
    {
        var startIndex = _numbers.IndexOf(_numbers.First(n => n.Number == 0));

        var sum = 0L;

        sum += _numbers[(1_000 + startIndex) % _numbers.Count].Number;

        sum += _numbers[(2_000 + startIndex) % _numbers.Count].Number;

        sum += _numbers[(3_000 + startIndex) % _numbers.Count].Number;

        return sum;
    }

    private void DoSwap(int index)
    {
        var number = _initialNumbers[index];

        var oldIndex = _numbers.IndexOf(number);

        var newIndex = (int) ((oldIndex + number.Number) % (_numbers.Count - 1));

        if (newIndex <= 0 && oldIndex + number.Number != 0)
        {
            newIndex = _numbers.Count - 1 + newIndex;
        }

        _numbers.RemoveAt(oldIndex);

        _numbers.Insert(newIndex, number);
    }
}