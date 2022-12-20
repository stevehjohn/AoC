using AoC.Solutions.Exceptions;

namespace AoC.Solutions.Solutions._2022._20;

public class CircularLinkedList<T>
{
    private Node<T> _start;

    private Node<T> _end;
    
    public void Add(T item)
    {
        var newItem = new Node<T>(item);

        if (_start == null)
        {
            newItem.Next = newItem;

            newItem.Previous = newItem;

            _start = newItem;

            _end = newItem;

            return;
        }

        var tempEnd = _end;

        _end.Next = newItem;

        _end = newItem;

        newItem.Previous = tempEnd;

        newItem.Next = _start;

        _start.Previous = _end;
    }

    public Node<T> Get(Func<T, bool> function)
    {
        var look = _start;

        do
        {
            if (function(look.Value))
            {
                return look;
            }

            look = look.Next;

        } while (look != _start);

        throw new PuzzleException("Not found in list.");
    }

    public void Swap(Node<T> left, Node<T> right)
    {
        var tempPrevious = left.Previous;

        var tempNext = left.Next;

        left.Next = right.Next;

        left.Previous = right.Previous;

        right.Next = tempNext;

        right.Previous = tempPrevious;
    }
}