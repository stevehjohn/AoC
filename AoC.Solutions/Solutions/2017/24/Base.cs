using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2017._24;

public abstract class Base : Solution
{
    public override string Description => "Electromagnetic moat";

    private readonly List<(int EdgeA, int EdgeB)> _components = [];

    private int _maxStrength;

    private int _maxLength;

    private int _strongestLongest;

    protected (int Strongest, int StrongestLongest) Solve()
    {
        var starts = _components.Where(c => c.EdgeA == 0 || c.EdgeB == 0).ToList();

        foreach (var component in starts)
        {
            var edge = component.EdgeA == 0 ? component.EdgeB : component.EdgeA;

            Solve(edge, edge, 0, _components.Where(c => c != component).ToList());
        }

        return (_maxStrength, _strongestLongest);
    }

    private void Solve(int edge, int strength, int length, List<(int EdgeA, int EdgeB)> remainingEdges)
    {
        _maxStrength = Math.Max(strength, _maxStrength);

        if (length == _maxLength)
        {
            _strongestLongest = Math.Max(strength, _strongestLongest);
        }

        if (length > _maxLength)
        {
            _maxLength = length;
            
            _strongestLongest = strength;
        }

        foreach (var component in remainingEdges)
        {
            if (component.EdgeA == edge || component.EdgeB == edge)
            {
                Solve(component.EdgeA == edge ? component.EdgeB : component.EdgeA, strength + component.EdgeA + component.EdgeB, length + 1, remainingEdges.Where(c => c != component).ToList());
            }
        }
    }

    protected void ParseInput()
    {
        foreach (var line in Input)
        {
            var parts = line.Split('/').Select(int.Parse).ToList();

            _components.Add((parts[0], parts[1]));
        }
    }
}