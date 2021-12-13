﻿using AoC.Solutions.Infrastructure;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._14;

[UsedImplicitly]
public class Part1 : Solution
{
    private readonly List<Reaction> _reactions = new();

    public override string GetAnswer()
    {
        ParseInput();

        var fuel = _reactions.Single(r => r.Result.Name == "FUEL");

        var total = GetOre(fuel);

        return total.ToString();
    }

    private void ParseInput()
    {
        foreach (var line in Input)
        {
            var io = line.Split("=>", StringSplitOptions.TrimEntries);

            var reaction = new Reaction
                           {
                               Result = ParseMatter(io[1])
                           };

            var input = io[0].Split(',', StringSplitOptions.TrimEntries);

            foreach (var matter in input)
            {
                reaction.Materials.Add(ParseMatter(matter));
            }

            _reactions.Add(reaction);
        }
    }

    private static Matter ParseMatter(string input)
    {
        var parts = input.Split(' ', StringSplitOptions.TrimEntries);

        return new Matter
               {
                   Amount = int.Parse(parts[0]),
                   Name = parts[1]
               };
    }

    private int GetOre(Reaction node)
    {
        //var total = 0;

        //foreach (var material in node.Materials)
        //{
        //    if (material.Name == "ORE")
        //    {
        //        return material.Amount;
        //    }

        //    total += material.Amount * GetOre(_reactions.Single(r => r.Result.Name == material.Name)) * node.Result.Amount;
        //}

        //return total;

        return 0;
    }
}