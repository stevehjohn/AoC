namespace AoC.Solutions.Solutions._2019._14;

public class Order
{
    public string IngredientName { get; }

    public long Amount { get; }

    public Order(string ingredientName, long amount)
    {
        IngredientName = ingredientName;

        Amount = amount;
    }
}