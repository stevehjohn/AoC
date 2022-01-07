namespace AoC.Solutions.Solutions._2019._14;

public class Component
{
    public int QuantityRequired { get; }

    public Material Material { get; }

    public Component(int quantityRequired, Material material)
    {
        QuantityRequired = quantityRequired;

        Material = material;
    }
}