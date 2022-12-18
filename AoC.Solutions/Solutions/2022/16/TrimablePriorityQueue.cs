using System.Numerics;

namespace AoC.Solutions.Solutions._2022._16;

public class TrimablePriorityQueue<T, TP> where TP : INumber<TP>, IComparable<TP>
{
    private readonly SortedList<TP, List<T>> _items = new();

    private int _count;

    public int Count => _count;

    public void Enqueue(T item, TP priority)
    {
        _count++;

        if (_items.ContainsKey(priority))
        {
            _items[priority].Add(item);

            return;
        }

        _items.Add(priority, new List<T> { item });
    }

    public T Dequeue()
    {
        _count--;

        var priorityItem = _items.GetValueAtIndex(0);

        var item = priorityItem[0];

        priorityItem.RemoveAt(0);

        if (priorityItem.Count == 0)
        {
            _items.RemoveAt(0);
        }

        return item;
    }

    public int Trim(Func<T, bool> function)
    {
        var trimmed = 0;

        var i1 = 0;

        while (i1 < _items.Count)
        {
            var priorityItem = _items.GetValueAtIndex(i1);

            var i2 = 0;

            while (i2 < priorityItem.Count)
            {
                if (function(priorityItem[i2]))
                {
                    priorityItem.RemoveAt(i2);

                    trimmed++;

                    _count--;

                    continue;
                }

                i2++;
            }

            if (priorityItem.Count == 0)
            {
                _items.RemoveAt(i1);

                continue;
            }

            i1++;
        }

        return trimmed;
    }
}