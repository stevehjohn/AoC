namespace AoC.Solutions.Solutions._2022._19;

public class TrimableQueue<T>
{
    private readonly LinkedList<T> _items = new();

    public int Count => _items.Count;

    public void Enqueue(T item)
    {
        _items.AddLast(item);
    }

    public T Dequeue()
    {
        var item = _items.First;

        _items.RemoveFirst();

        // ReSharper disable once PossibleNullReferenceException
        return item.Value;
    }

    public int Trim(Func<T, bool> function)
    {
        var count = 0;

        var item = _items.First;

        while (item != null)
        {
            var next = item.Next;

            // ReSharper disable once PossibleNullReferenceException
            if (function(item.Value))
            {
                _items.Remove(item);

                count++;
            }

            item = next;
        }

        return count;
    }
}