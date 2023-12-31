using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2019._14;

public abstract class Base : Solution
{
    public override string Description => "Replicator";

    private readonly Dictionary<string, Recipe> _recipes = new();

    private readonly Dictionary<string, long> _stock = new();

    protected long CreateFuel(int amount = 1)
    {
        var ore = 0L;

        var orders = new Queue<Order>();

        orders.Enqueue(new Order("FUEL", amount));

        while (orders.Count > 0)
        {
            var order = orders.Dequeue();

            if (order.IngredientName == "ORE")
            {
                ore += order.Amount;

                continue;
            }

            if (order.Amount <= _stock[order.IngredientName])
            {
                _stock[order.IngredientName] -= order.Amount;

                continue;
            }

            var required = order.Amount - _stock[order.IngredientName];

            var recipe = _recipes[order.IngredientName];

            var batches = (long) Math.Ceiling((decimal) required / recipe.AmountProduced);

            foreach (var ingredient in recipe.Ingredients)
            {
                orders.Enqueue(new Order(ingredient.Name, ingredient.AmountRequired * batches));
            }

            _stock[order.IngredientName] = batches * recipe.AmountProduced - required;
        }

        return ore;
    }

    protected void ResetStock()
    {
        foreach (var item in _stock)
        {
            _stock[item.Key] = 0;
        }
    }

    protected void ParseInput()
    {
        foreach (var line in Input)
        {
            var parts = line.Split("=>", StringSplitOptions.TrimEntries);

            var recipeData = ParseItem(parts[1]);

            var recipe = new Recipe(recipeData.Name, recipeData.Quantity);

            _stock.Add(recipe.Name, 0);

            var ingredientsData = parts[0].Split(',', StringSplitOptions.TrimEntries);

            foreach (var ingredientData in ingredientsData)
            {
                var ingredient = ParseItem(ingredientData);

                recipe.Ingredients.Add(new Ingredient(ingredient.Name, ingredient.Quantity));
            }

            _recipes.Add(recipe.Name, recipe);
        }
    }

    private static (string Name, int Quantity) ParseItem(string input)
    {
        var split = input.Split(' ', StringSplitOptions.TrimEntries);

        return (split[1], int.Parse(split[0]));
    }
}