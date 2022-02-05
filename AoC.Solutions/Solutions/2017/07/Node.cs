﻿namespace AoC.Solutions.Solutions._2017._07;

public class Node
{
    public string Name { get; }

    public int Weight { get; set; }

    public Node Parent { get; set; }

    public List<Node> Children { get; } = new();

    public Node(string name, int weight)
    {
        Name = name;

        Weight = weight;
    }
}