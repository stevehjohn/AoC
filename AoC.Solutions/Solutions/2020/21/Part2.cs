using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2020._21;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var ingredientAllergens = IdentifyIngredientAllergens();

        var result = string.Join(',', ingredientAllergens.OrderBy(ia => ia.Allergen).Select(ia => ia.Ingredient));

        return result;
    }

    private List<(string Ingredient, string Allergen)> IdentifyIngredientAllergens()
    {
        var identifiedAllergens = new HashSet<string>();

        while (true)
        {
            var allergensToCheck = Allergens.Where(a => identifiedAllergens.All(ia => ia != a.Key));

            foreach (var allergen in allergensToCheck)
            {
                if (allergen.Value.Count == 1)
                {
                    var allergenIngredient = allergen.Value.Single();

                    identifiedAllergens.Add(allergen.Key);

                    foreach (var otherAllergen in Allergens)
                    {
                        if (otherAllergen.Key == allergen.Key)
                        {
                            continue;
                        }

                        otherAllergen.Value.Remove(allergenIngredient);
                    }
                }
            }

            if (Allergens.All(a => a.Value.Count == 1))
            {
                return Allergens.Select(a => (Ingredient: a.Value.Single(), Allergen: a.Key)).ToList();
            }
        }
    }
}