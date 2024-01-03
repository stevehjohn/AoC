using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2015._15;

public abstract class Base : Solution
{
    public override string Description => "Science for hungry people";

    private readonly List<(int Capacity, int Durability, int Flavour, int Texture, int Calories)> _ingredients = new();

    protected int Solve(int calorieTarget = -1)
    {
        var highest = 0;

        for (var a = 0; a < 101; a++)
        {
            for (var b = 0; b < 101 - a; b++)
            {
                for (var c = 0; c < 101 - b - a; c++)
                {
                    var d = 100 - a - b - c;

                    var capacity = _ingredients[0].Capacity * a + _ingredients[1].Capacity * b + _ingredients[2].Capacity * c + _ingredients[3].Capacity * d;

                    var durability = _ingredients[0].Durability * a + _ingredients[1].Durability * b + _ingredients[2].Durability * c + _ingredients[3].Durability * d;
                
                    var flavour = _ingredients[0].Flavour * a + _ingredients[1].Flavour * b + _ingredients[2].Flavour * c + _ingredients[3].Flavour * d;
                    
                    var texture = _ingredients[0].Texture * a + _ingredients[1].Texture * b + _ingredients[2].Texture * c + _ingredients[3].Texture * d;

                    var calories = _ingredients[0].Calories * a + _ingredients[1].Calories * b + _ingredients[2].Calories * c + _ingredients[3].Calories * d;

                    if (capacity < 0 || durability < 0 || flavour < 0 || texture < 0)
                    {
                        continue;
                    }

                    var score = capacity * durability * flavour * texture;

                    if (score > highest && (calorieTarget == -1 || calories == calorieTarget))
                    {
                        highest = score;
                    }
                }
            }
        }

        return highest;
    }

    protected void ParseInput()
    {
        foreach (var line in Input)
        {
            _ingredients.Add(ParseLine(line));
        }
    }

    private static (int Capacity, int Durability, int Flavour, int Texture, int Calories) ParseLine(string line)
    {
        var parts = line.Split(new[] { ' ', ',' }, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

        return (int.Parse(parts[2]), int.Parse(parts[4]), int.Parse(parts[6]), int.Parse(parts[8]), int.Parse(parts[10]));
    }
}