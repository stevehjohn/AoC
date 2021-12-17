namespace AoC.Solutions.Common;

public class PriorityQueue<T>
{
    private readonly List<(int Priority, T Entry)> _entries;

    public PriorityQueue()
    {
        _entries = new();
    }

    public void Push(int priority, T item)
    {
        _entries.Add((priority, item));
    }

    public T Pop()
    {
        var min = _entries.Min(e => e.Priority);

        return _entries.First(e => e.Priority == min).Entry;
    }
}