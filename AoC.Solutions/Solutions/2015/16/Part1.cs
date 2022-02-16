using AoC.Solutions.Exceptions;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2015._16;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        // ReSharper disable StringLiteralTypo
        var imprint = new Dictionary<string, int>
                      {
                          { "children", 3 },
                          { "cats", 7 },
                          { "samoyeds", 2 },
                          { "pomeranians", 3 },
                          { "akitas", 0 },
                          { "vizslas", 0 },
                          { "goldfish", 5 },
                          { "trees", 3 },
                          { "cars", 2 },
                          { "perfumes", 1 }
                      };
        // ReSharper restore StringLiteralTypo

        foreach (var sue in Input)
        {
            var parts = sue.Split(new[] { ' ', ',', ':' }, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            if (imprint[parts[2]] == int.Parse(parts[3]) && imprint[parts[4]] == int.Parse(parts[5]) && imprint[parts[6]] == int.Parse(parts[7]))
            {
                return parts[1];
            }
        }

        throw new PuzzleException("Solution not found.");
    }
}