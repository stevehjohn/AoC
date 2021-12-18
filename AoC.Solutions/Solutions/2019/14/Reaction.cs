namespace AoC.Solutions.Solutions._2019._14;

public class Reaction
{
    public string Name { get; }

    public int AmountCreated { get; }

    public Dictionary<string, int> ComponentsRequired { get; }

    public Reaction(string name, int amountCreated)
    {
        Name = name;

        AmountCreated = amountCreated;

        ComponentsRequired = new Dictionary<string, int>();
    }
}