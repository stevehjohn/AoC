using AoC.Solutions.Exceptions;

namespace AoC.Solutions.Solutions._2022._20;

public class CircularLinkedList<T>
{
    private Node<T> _start;

    private Node<T> _end;

    public Node<T> First => _start;

    private int _length;

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

        _length++;
    }

    public Node<T> Get(Func<T, bool> function)
    {
        var look = _start;

        do
        {
            if (function(look.Data))
            {
                return look;
            }

            look = look.Next;

        } while (look != _start);

        throw new PuzzleException("Not found in list.");
    }

    public void Move(Node<T> node, int places)
    {
        var delta = places > 0 ? 1 : -1;

        for (var i = 0; i != places; i += delta)
        {
            if (delta < 0)
            {
                (node.Previous.Data, node.Data) = (node.Data, node.Previous.Data);

                node = node.Previous;

                if (node == _start)
                {
                    _start = _start.Next;
                }
            }
            else
            {
                (node.Data, node.Next.Data) = (node.Next.Data, node.Data);

                node = node.Next;

                if (node == _start)
                {
                    _start = _start.Previous;
                }
            }
        }
    }
}