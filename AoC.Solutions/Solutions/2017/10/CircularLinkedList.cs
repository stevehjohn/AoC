namespace AoC.Solutions.Solutions._2017._10;

public class CircularLinkedList<T>
{
    private CircularLinkedListNode<T> _first;
    
    private CircularLinkedListNode<T> _last;

    public CircularLinkedListNode<T> Add(T value)
    {
        var newNode = new CircularLinkedListNode<T>(value);

        if (_first == null)
        {
            _first = newNode;

            _last = newNode;

            newNode.Next = newNode;

            newNode.Previous = newNode;
        }
        else
        {
            newNode.Previous = _last;

            _last.Next = newNode;

            _last = newNode;

            newNode.Next = _first;

            _first.Previous = _last;
        }

        return newNode;
    }
}

public class CircularLinkedListNode<T>
{
    public T Value { get; }

    public CircularLinkedListNode<T> Previous { get; set; }

    public CircularLinkedListNode<T> Next { get; set; }

    public CircularLinkedListNode(T value)
    {
        Value = value;
    }
}