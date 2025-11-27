namespace AoC.Solutions.Extensions;

public static class LinkedListExtensions
{
    extension<T>(LinkedListNode<T> node)
    {
        public LinkedListNode<T> NextCircular()
        {
            if (node.List == null)
            {
                throw new NullReferenceException("Node is not linked to a list");
            }

            return node.Next ?? node.List.First;
        }

        public LinkedListNode<T> PreviousCircular()
        {
            if (node.List == null)
            {
                throw new NullReferenceException("Node is not linked to a list");
            }

            return node.Previous ?? node.List.Last;
        }
    }
}