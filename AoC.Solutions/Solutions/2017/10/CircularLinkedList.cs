namespace AoC.Solutions.Solutions._2017._10;

public class CircularLinkedList<T>
{
    public CircularLinkedListNode<T> First { get; private set; }

    public CircularLinkedListNode<T> Add(T value)
    {
        var newNode = new CircularLinkedListNode<T>(value);

        if (First == null)
        {
            First = newNode;

            newNode.Next = newNode;

            newNode.Previous = newNode;
        }
        else
        {
            newNode.Previous = First.Previous;

            newNode.Next = First;

            First.Previous.Next = newNode;

            First.Previous = newNode;
        }

        return newNode;
    }

    public void Swap(CircularLinkedListNode<T> left, CircularLinkedListNode<T> right)
    {
        var sectionPrevious = left.Previous;

        var sectionNext = right.Next;

        // Detatch
        left.Previous = null;

        right.Next = null;

        // Reverse
        var node = left;

        while (node != null)
        {
            (node.Previous, node.Next) = (node.Next, node.Previous);

            node = node.Previous;
        }

        // Reattach
        if (sectionPrevious == right)
        {
            left.Next = right;

            right.Previous = left;
        }
        else
        {
            sectionPrevious.Next = right;

            right.Previous = sectionPrevious;

            sectionNext.Previous = left;

            left.Next = sectionNext;
        }

        // TODO: Update First
    }
}