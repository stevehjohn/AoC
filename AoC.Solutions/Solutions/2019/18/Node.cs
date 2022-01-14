using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2019._18;

public class Node
{
    public char Name { get; }

    public Point Position { get; }

    public List<Distance> Distances { get; }

    public Node(char name, Point position)
    {
        Name = name;

        Position = position;

        Distances = new List<Distance>();
    }
}

public class Distance
{
    public int Steps { get; }

    public Node Destination { get; }

    public Distance(int steps, Node destination)
    {
        Steps = steps;

        Destination = destination;
    }
}