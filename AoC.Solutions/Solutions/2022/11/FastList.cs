using AoC.Solutions.Exceptions;

namespace AoC.Solutions.Solutions._2022._11;

public class FastList<T>
{
    private readonly int _capacity;

    private readonly T[] _items;

    private int _start;

    private int _end;

    private int _count;

    private readonly int _mask;

    public FastList(int capacity)
    {
        if (capacity % 2 != 0)
        {
            throw new PuzzleException("Capacity must be a power of 2");
        }

        _capacity = capacity;

        _mask = capacity - 1;

        _items = new T[_capacity];
    }

    public void Add(T item)
    {
        _items[_end] = item;

        _end = (_end + 1) & _mask;

        _count++;
    }

    public void RemoveFirst()
    {
        _start = (_start + 1) & _mask;

        _count--;
    }

    public T First => _items[_start % _capacity];

    public int Count => _count;
}