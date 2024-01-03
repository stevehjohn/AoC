namespace AoC.Solutions.Solutions._2019._14;

public class Ingredient
{
    public string Name { get; }

    public int AmountRequired { get; }

    public Ingredient(string name, int amountRequired)
    {
        Name = name;

        AmountRequired = amountRequired;
    }
}