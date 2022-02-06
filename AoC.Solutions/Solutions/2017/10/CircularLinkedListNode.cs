namespace AoC.Solutions.Solutions._2017._10;

public class CircularLinkedListNode<T>
{
    public T Value { get; }

    public CircularLinkedListNode<T> Previous { get; set; }

    public CircularLinkedListNode<T> Next { get; set; }

    public CircularLinkedListNode(T value)
    {
        Value = value;
    }

    public CircularLinkedListNode<T> Skip(int skip)
    {
        var node = this;

        for (var i = 0; i < skip; i++)
        {
            node = node.Next;
        }

        return node;
    }
}