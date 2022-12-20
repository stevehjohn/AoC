using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2022._20;

public abstract class Base : Solution
{
    public override string Description => "Grove positioning system";

    private readonly CircularLinkedList<(int Value, int InitialIndex)> _numbers = new();

    private int _length;

    protected void ParseInput()
    {
        _length = Input.Length;

        for (var i = 0; i < _length; i++)
        {
            _numbers.Add((int.Parse(Input[i]), i));
        }
    }

    protected void MixState()
    {
        for (var i = 0; i < _length; i++)
        {
            var iCaptured = i;

            var source = _numbers.Get(n => n.InitialIndex == iCaptured);

            var target = source;

            for (var t = 0; t < Math.Abs(source.Value.Value); t++)
            {
                if (source.Value.Value < 0)
                {
                    target = target.Previous;
                }
                else
                {
                    target = target.Next;
                }
            }

            _numbers.Swap(source, target);
        }
    }
}