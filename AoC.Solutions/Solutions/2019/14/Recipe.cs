namespace AoC.Solutions.Solutions._2019._14;

public class Recipe
{
    public string Name { get; }

    public int AmountProduced { get; }

    public List<Ingredient> Ingredients { get; }

    public Recipe(string name, int amountProduced)
    {
        Name = name;

        AmountProduced = amountProduced;

        Ingredients = new List<Ingredient>();
    }
}