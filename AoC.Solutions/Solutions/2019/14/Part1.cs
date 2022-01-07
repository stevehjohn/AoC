using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._14;

[UsedImplicitly]
public class Part1 : Base
{
    private int _totalOre;

    public override string GetAnswer()
    {
        ParseInput();

        CalculateRequiredOre(RootMaterial, 1);

        return _totalOre.ToString();
    }

    private void CalculateRequiredOre(Material material, int quantityRequired)
    {
        if (material.Stock >= quantityRequired)
        {
            material.Stock -= quantityRequired;

            return;
        }

        foreach (var component in material.Components)
        {
            if (component.Material.Name == BaseMaterialName)
            {
                while (material.Stock < quantityRequired)
                {
                    material.Stock += material.QuantityProduced;

                    _totalOre += component.QuantityRequired;
                }

                material.Stock -= quantityRequired;

                continue;
            }

            CalculateRequiredOre(component.Material, component.QuantityRequired * quantityRequired);
        }
    }
}