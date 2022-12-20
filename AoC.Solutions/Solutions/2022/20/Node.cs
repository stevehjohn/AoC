namespace AoC.Solutions.Solutions._2022._20;

public class Node<T>
{
    public T Value { get; }

    public Node<T> Previous { get; set; }

    public Node<T> Next { get; set; }

    public Node(T value)
    {
        Value = value;
    }
}