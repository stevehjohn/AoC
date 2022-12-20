namespace AoC.Solutions.Solutions._2022._20;

public class CircularLinkedList<T>
{
    private Node<T> _current;
    
    private Node<T> _start;

    public void Add(T item)
    {
        var newItem = new Node<T>(item);

        if (_current == null)
        {
            newItem.Previous = newItem;

            newItem.Next = newItem;

            _current = newItem;

            _start = newItem;

            return;
        }

        newItem.Next = _start;

        _current.Next = newItem;

        _current = newItem;
    }
}