using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2022._20;

public abstract class Base : Solution
{
    public override string Description => "Grove positioning system";

    private readonly CircularLinkedList<int> _numbers = new();

    protected void ParseInput()
    {
        for (var i = 0; i < Input.Length; i++)
        {
            _numbers.Add(int.Parse(Input[i]));
        }
    }

    protected void MixState()
    {
        //for (var i = 0; i < _length; i++)
        //{
        //    _start += _numbers[i];
        //}
    }
}