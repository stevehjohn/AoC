using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2020._21;

[UsedImplicitly]
public class Part1 : Base
{
    private readonly Dictionary<string, List<string>> _allergens = new();

    public override string GetAnswer()
    {
        ParseInput();

        DetermineAllergens();

        return "TESTING";
    }

    private void DetermineAllergens()
    {
    }

    private void ParseInput()
    {
        foreach (var line in Input)
        {
            var split = line[..^1].Split("(contains", StringSplitOptions.TrimEntries);

            var ingredients = split[0].Split(' ');

            var allergens = split[1].Split(", ");

            foreach (var allergen in allergens)
            {
                if (! _allergens.TryGetValue(allergen, out var allergenIngredients))
                {
                    allergenIngredients = new List<string>();

                    _allergens.Add(allergen, allergenIngredients);
                }

                foreach (var ingredient in ingredients)
                {
                    if (! allergenIngredients.Contains(ingredient))
                    {
                        allergenIngredients.Add(ingredient);
                    }
                }
            }
        }
    }
}