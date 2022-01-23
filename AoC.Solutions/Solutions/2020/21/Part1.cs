using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2020._21;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var allergens = GetAllergens();

        var result = 0;

        foreach (var ingredient in IngredientOccurrences)
        {
            if (! allergens.Contains(ingredient.Key))
            {
                result += ingredient.Value;
            }
        }

        return result.ToString();
    }

    private List<string> GetAllergens()
    {
        var allergens = Allergens.SelectMany(a => a.Value).Distinct().ToList();

        return allergens;
    }
}