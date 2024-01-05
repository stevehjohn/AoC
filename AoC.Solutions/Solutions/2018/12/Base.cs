using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2018._12;

public abstract class Base : Solution
{
    public override string Description => "Plant potty";

    protected List<int> PotsWithPlants = [];

    private readonly List<(bool[] Pattern, bool Spawn)> _rules = [];

    protected void RunGeneration()
    {
        var newState = new List<int>();

        for (var i = PotsWithPlants.Min() - 2; i < PotsWithPlants.Max() + 3; i++)
        {
            if (ShouldContainPlant(i))
            {
                newState.Add(i);
            }
        }

        PotsWithPlants = newState;
    }

    private bool ShouldContainPlant(int pot)
    {
        foreach (var rule in _rules)
        {
            if (RuleMatches(rule.Pattern, pot))
            {
                return rule.Spawn;
            }
        }

        return false;
    }

    private bool RuleMatches(bool[] pattern, int pot)
    {
        for (var x = 0; x < 5; x++)
        {
            if (pattern[x] && ! PotsWithPlants.Contains(pot - 2 + x) || ! pattern[x] && PotsWithPlants.Contains(pot - 2 + x))
            {
                return false;
            }
        }

        return true;
    }

    protected void ParseInput()
    {
        var initialState = Input[0][15..];

        for (var i = 0; i < initialState.Length; i++)
        {
            if (initialState[i] == '#')
            {
                PotsWithPlants.Add(i);
            }
        }

        foreach (var line in Input.Skip(2))
        {
            _rules.Add(ParseRule(line));
        }
    }

    private static (bool[], bool) ParseRule(string line)
    {
        var split = line.Split("=>", StringSplitOptions.TrimEntries);

        var pots = new bool[5];

        for (var i = 0; i < split[0].Length; i++)
        {
            pots[i] = split[0][i] == '#';
        }

        return (pots, split[1] == "#");
    }
}