using AoC.Solutions.Exceptions;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2015._16;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        // ReSharper disable StringLiteralTypo
        var imprint = new Dictionary<string, Func<int, bool>>
                      {
                          { "children", c => c == 3 },
                          { "cats", c => c > 7 },
                          { "samoyeds", c => c == 2 },
                          { "pomeranians", c => c < 3 },
                          { "akitas", c => c == 0 },
                          { "vizslas", c => c == 0 },
                          { "goldfish", c => c < 5 },
                          { "trees", c => c > 3 },
                          { "cars", c => c == 2 },
                          { "perfumes", c => c == 1 }
                      };
        // ReSharper restore StringLiteralTypo

        foreach (var sue in Input)
        {
            var parts = sue.Split(new[] { ' ', ',', ':' }, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            if (imprint[parts[2]](int.Parse(parts[3])) && imprint[parts[4]](int.Parse(parts[5])) && imprint[parts[6]](int.Parse(parts[7])))
            {
                return parts[1];
            }
        }

        throw new PuzzleException("Solution not found.");
    }
}