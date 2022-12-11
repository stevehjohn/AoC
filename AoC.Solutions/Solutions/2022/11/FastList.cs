namespace AoC.Solutions.Solutions._2022._11;

public class FastList<T>
{
    private readonly int _capacity;

    private readonly T[] _items;

    private int _start;

    private int _end;

    private int _count;

    public FastList(int capacity)
    {
        _capacity = capacity;

        _items = new T[_capacity];
    }

    public void Add(T item)
    {
        _items[_end] = item;

        _end++;

        if (_end >= _capacity)
        {
            _end = 0;
        }

        _count++;
    }

    public void RemoveFirst()
    {
        _start++;

        if (_start >= _capacity)
        {
            _start = 0;
        }

        _count--;
    }

    public T this[int index] => _items[(_start + index) % _capacity];

    public int Count => _count;
}