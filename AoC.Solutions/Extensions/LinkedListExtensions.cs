namespace AoC.Solutions.Extensions;

public static class LinkedListExtensions
{
    public static LinkedListNode<T> NextCircular<T>(this LinkedListNode<T> node)
    {
        if (node.List == null)
        {
            throw new NullReferenceException("Node is not linked to a list");
        }

        return node.Next ?? node.List.First;
    }

    public static LinkedListNode<T> PreviousCircular<T>(this LinkedListNode<T> node)
    {
        if (node.List == null)
        {
            throw new NullReferenceException("Node is not linked to a list");
        }

        return node.Previous ?? node.List.Last;
    }
}