namespace AoC.Solutions.Solutions._2019._14;

public class Material
{
    public string Name { get; }

    public int AmountRequired { get; }

    public List<Material> Components { get; }

    public Material(string name, int amountRequired)
    {
        Name = name;

        AmountRequired = amountRequired;

        Components = new List<Material>();
    }
}