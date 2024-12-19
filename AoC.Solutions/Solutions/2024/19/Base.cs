using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2024._19;

public abstract class Base : Solution
{
    public override string Description => "Linen layout";

    private string[] _towels;

    private string[] _designs;

    private readonly HashSet<string> _possibles = [];
    
    protected void ParseInput()
    {
        _towels = Input[0].Split(',', StringSplitOptions.TrimEntries);

        _designs = Input[2..];
    }

    protected int CountPossibilities()
    {
        var count = 0;

        for (var i = 0; i < _designs.Length; i++)
        {
            var isPossible = IsPossible(_designs[i]);
            
            count += isPossible ? 1 : 0;
        }

        return count;
    }

    private bool IsPossible(string design)
    {
        if (_possibles.Contains(design))
        {
            return true;
        }
        
        for (var i = 0; i < _towels.Length; i++)
        {
            var towel = _towels[i];

            if (towel.Length > design.Length)
            {
                continue;
            }

            if (design.EndsWith(towel))
            {
                _possibles.Add(design);

                if (towel.Length == design.Length)
                {
                    return true;
                }

                if (IsPossible(design[..^towel.Length]))
                {
                    return true;
                }
            }
        }

        return false;
    }
}