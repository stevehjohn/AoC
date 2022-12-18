using System.Numerics;

namespace AoC.Solutions.Solutions._2022._16;

public class TrimablePriorityQueue<T, TP> where TP : INumber<TP>, IComparable<TP>
{
    private readonly List<(TP Priority, T Item)> _items = new();

    public int Count => _items.Count;

    public void Enqueue(T item, TP priority)
    {
        if (_items.Count == 0)
        {
            _items.Add((priority, item));

            return;
        }

        var index = 0;

        var endIndex = _items.Count;

        while (endIndex > index)
        {
            var windowSize = endIndex - index;

            var middleIndex = index + windowSize / 2;

            var middleValue = _items[middleIndex].Priority;

            var compareResult = middleValue.CompareTo(priority);

            if (compareResult == 0)
            {
                _items.Insert(middleIndex, (priority, item));

                return;
            }
            
            if (compareResult < 0)
            {
                index = middleIndex + 1;

                continue;
            }
            
            endIndex = middleIndex;
        }

        _items.Insert(index, (priority, item));
    }

    public T Dequeue()
    {
        var item = _items[0];

        _items.RemoveAt(0);

        return item.Item;
    }

    public int Trim(Func<T, bool> function)
    {
        var count = 0;

        var i = 0;

        while (i < _items.Count)
        {
            if (function(_items[i].Item))
            {
                _items.RemoveAt(i);

                count++;

                continue;
            }

            i++;
        }

        return count;
    }
}