using AoC.Solutions.Exceptions;

namespace AoC.Solutions.Solutions._2022._11;

public class FastList<T>
{
    private readonly T[] _items;

    private int _start;

    private int _end;

    private readonly int _mask;

    public int Count { get; private set; }

    public FastList(int capacity)
    {
        if (capacity % 2 != 0)
        {
            throw new PuzzleException("Capacity must be a power of 2");
        }

        _mask = capacity - 1;

        _items = new T[capacity];
    }

    public void Add(T item)
    {
        _items[_end] = item;

        _end = (_end + 1) & _mask;

        Count++;
    }

    public T RemoveFirst()
    {
        var start = _start;

        _start = (_start + 1) & _mask;

        Count--;

        return _items[start];
    }
}