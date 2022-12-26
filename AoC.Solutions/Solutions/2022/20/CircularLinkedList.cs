using AoC.Solutions.Exceptions;

namespace AoC.Solutions.Solutions._2022._20;

public class CircularLinkedList
{
    private Node _start;

    private Node _end;

    private int _length;

    private readonly Node[] _nodesByInitialIndex = new Node[5_000];

    public void Add(long value, int initialIndex)
    {
        var newItem = new Node 
                      { 
                          Value = value,
                          InitialIndex = initialIndex
                      };

        _nodesByInitialIndex[initialIndex] = newItem;

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

    public Node GetByInitialIndex(int initialIndex)
    {
        return _nodesByInitialIndex[initialIndex];
    }

    public Node GetByValue(long value)
    {
        var look = _start;

        do
        {
            if (look.Value == value)
            {
                return look;
            }

            look = look.Next;

        } while (look != _start);

        throw new PuzzleException("Not found in list.");
    }

    public void Move(Node node, long places)
    {
        places %= _length;

        var delta = places > 0 ? 1 : -1;

        for (var i = 0; i != places; i += delta)
        {
            if (delta < 0)
            {
                (node.Previous.Value, node.Value) = (node.Value, node.Previous.Value);

                node = node.Previous;
            }
            else
            {
                (node.Value, node.Next.Value) = (node.Next.Value, node.Value);

                node = node.Next;
            }
        }
    }
}