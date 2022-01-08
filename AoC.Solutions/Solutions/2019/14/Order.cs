namespace AoC.Solutions.Solutions._2019._14;

public class Order
{
    public string IngredientName { get; }

    public int Amount { get; }

    public Order(string ingredientName, int amount)
    {
        IngredientName = ingredientName;

        Amount = amount;
    }
}