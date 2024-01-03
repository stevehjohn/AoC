using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2020._21;

public abstract class Base : Solution
{
    public override string Description => "Allergen detective";

    protected readonly Dictionary<string, List<string>> Allergens = new();

    protected readonly Dictionary<string, int> IngredientOccurrences = new();

    protected void ParseInput()
    {
        foreach (var line in Input)
        {
            var split = line[..^1].Split("(contains", StringSplitOptions.TrimEntries);

            var ingredients = split[0].Split(' ');

            foreach (var ingredient in ingredients)
            {
                if (! IngredientOccurrences.TryAdd(ingredient, 1))
                {
                    IngredientOccurrences[ingredient]++;
                }
            }

            var allergens = split[1].Split(", ");

            foreach (var allergen in allergens)
            {
                if (! Allergens.TryGetValue(allergen, out var allergenIngredients))
                {
                    allergenIngredients = new List<string>();

                    Allergens.Add(allergen, allergenIngredients);

                    allergenIngredients.AddRange(ingredients);

                    continue;
                }

                Allergens[allergen] = allergenIngredients.Intersect(ingredients).ToList();
            }
        }
    }
}