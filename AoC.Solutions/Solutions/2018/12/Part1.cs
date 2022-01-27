namespace AoC.Solutions.Solutions._2018._12;

public class Part1 : Base
{
    private List<int> _potsWithPlants = new();

    private readonly List<(bool[] Pattern, bool Spawn)> _rules = new();

    public override string GetAnswer()
    {
        ParseInput();

        Dump();

        for (var g = 0; g < 20; g++)
        {
            RunGeneration();

            Dump();
        }

        return _potsWithPlants.Sum().ToString();
    }

    private void RunGeneration()
    {
        var newState = new List<int>();

        for (var i = _potsWithPlants.Min() - 2; i < _potsWithPlants.Max() + 3; i++)
        {
            if (ShouldContainPlant(i))
            {
                newState.Add(i);
            }
        }

        _potsWithPlants = newState;
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
            if (pattern[x] && ! _potsWithPlants.Contains(pot - 2 + x) || ! pattern[x] && _potsWithPlants.Contains(pot - 2 + x))
            {
                return false;
            }
        }

        return true;
    }

    private void Dump()
    {
        for (var i = _potsWithPlants.Min() - 2; i < _potsWithPlants.Max() + 3; i++)
        {
            if (_potsWithPlants.Contains(i))
            {
                Console.Write('#');
            }
            else
            {
                Console.Write('.');
            }
        }

        Console.WriteLine();
    }

    private void ParseInput()
    {
        var initialState = Input[0][15..];

        for (var i = 0; i < initialState.Length; i++)
        {
            if (initialState[i] == '#')
            {
                _potsWithPlants.Add(i);
            }
        }

        foreach (var line in Input.Skip(2))
        {
            _rules.Add(ParseRule(line));
        }
    }

    private (bool[], bool) ParseRule(string line)
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