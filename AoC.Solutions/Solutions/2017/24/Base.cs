using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2017._24;

public abstract class Base : Solution
{
    public override string Description => "Electromagnetic moat";

    private readonly List<(int A, int B)> _components = [];

    private int _maxStrength;

    private int _maxLength;

    private int _strongestLongest;

    private readonly Dictionary<int, List<int>> _byPort = [];

    protected (int Strongest, int StrongestLongest) Solve()
    {
        _maxStrength = 0;
        
        _maxLength = 0;
        
        _strongestLongest = 0;

        Search(0, UInt128.Zero, 0, 0);

        return (_maxStrength, _strongestLongest);
    }

    private void Search(int port, UInt128 used, int strength, int length)
    {
        if (strength > _maxStrength)
        {
            _maxStrength = strength;
        }

        if (length > _maxLength)
        {
            _maxLength = length;
            
            _strongestLongest = strength;
        }
        else if (length == _maxLength && strength > _strongestLongest)
        {
            _strongestLongest = strength;
        }

        if (! _byPort.TryGetValue(port, out var candidates))
        {
            return;
        }

        foreach (var index in candidates)
        {
            var bit = UInt128.One << index;

            if ((used & bit) != 0)
            {
                continue;
            }

            var (a, b) = _components[index];

            Search(a == port ? b : a, used | bit, strength + a + b, length + 1);
        }
    }

    protected void ParseInput()
    {
        _components.Clear();
        
        _byPort.Clear();

        foreach (var line in Input)
        {
            var parts = line.Split('/');

            var a = int.Parse(parts[0]);
            
            var b = int.Parse(parts[1]);

            var index = _components.Count;

            if (index >= 128)
            {
                throw new InvalidOperationException("More than 128 components.");
            }

            _components.Add((a, b));

            AddPort(a, index);

            if (a != b)
            {
                AddPort(b, index);
            }
        }
    }

    private void AddPort(int port, int index)
    {
        if (! _byPort.TryGetValue(port, out var list))
        {
            list = [];
            
            _byPort.Add(port, list);
        }

        list.Add(index);
    }
}