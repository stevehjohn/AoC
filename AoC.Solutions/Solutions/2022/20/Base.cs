using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2022._20;

public abstract class Base : Solution
{
    public override string Description => "Grove positioning system";

    private readonly CircularLinkedList _numbers = new();

    private int _length;

    protected void ParseInput(long key = 1)
    {
        _length = Input.Length;

        for (var i = 0; i < _length; i++)
        {
            _numbers.Add(long.Parse(Input[i]) * key, i);
        }
    }

    protected void MixState(int times = 1)
    {
        for (var t = 0; t < times; t++)
        {
            for (var i = 0; i < _length; i++)
            {
                var source = _numbers.GetByInitialIndex(i);

                _numbers.Move(source, source.Value);
            }
        }
    }

    protected long Solve()
    {
        var start = _numbers.GetByValue(0);

        var sum = 0L;

        for (var i = 0; i < 3_000; i++)
        {
            start = start.Next;

            if (i % 1000 == 999)
            {
                sum += start.Value;
            }
        }
        
        return sum;
    }
}