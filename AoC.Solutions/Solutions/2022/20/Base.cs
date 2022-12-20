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

            _numbers.Move(source, source.Data.Value);

            //Output();
        }
    }

    protected int Solve()
    {
        var start = _numbers.Get(n => n.Value == 0);

        var sum = 0;

        for (var i = 0; i < 3_000; i++)
        {
            start = start.Next;

            if (i % 1000 == 999)
            {
                sum += start.Data.Value;
            }
        }
        
        return sum;
    }

    private void Output()
    {
        var s = _numbers.First;

        for (var l = 0; l < 7; l++)
        {
            Console.Write($"{s.Data.Value, 2} ");

            s = s.Next;
        }

        Console.WriteLine();
    }
}