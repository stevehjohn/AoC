namespace AoC.Solutions.Solutions._2018._12;

public class Part1 : Base
{
    private readonly List<int> _potsWithPlants = new();

    private readonly List<(bool[], bool)> _rules = new();

    public override string GetAnswer()
    {
        ParseInput();

        return "TESTING";
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