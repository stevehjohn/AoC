namespace AoC.Solutions.Solutions._2019._14;

public class Material
{
    public string Name { get; }

    public int QuantityProduced { get; }

    public int Stock { get; set; }

    public List<Component> Components { get; }

    public Material(string name, int quantityProduced)
    {
        Name = name;

        QuantityProduced = quantityProduced;

        Components = new List<Component>();
    }
}