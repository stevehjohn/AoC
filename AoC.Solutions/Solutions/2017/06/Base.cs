using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2017._06;

public abstract class Base : Solution
{
    public override string Description => "Garbage collection";

    private readonly Dictionary<int, int> _banks = new();

    protected int RunUntilRepeats()
    {
        var states = new HashSet<int>();

        var cycle = 0;

        while (true)
        {
            cycle++;

            Reallocate();

            var hash = 0;

            foreach (var (_, value) in _banks)
            {
                hash = HashCode.Combine(hash, value);
            }

            if (! states.Add(hash))
            {
                break;
            }
        }

        return cycle;
    }

    private void Reallocate()
    {
        var largest = _banks.MaxBy(b => b.Value);

        var blocks = largest.Value;

        var index = largest.Key;

        _banks[index] = 0;

        while (blocks > 0)
        {
            index++;

            if (index == _banks.Count)
            {
                index = 0;
            }

            _banks[index]++;

            blocks--;
        }
    }

    protected void ParseInput()
    {
        var banks = Input[0].Split('\t', StringSplitOptions.TrimEntries);

        for (var i = 0; i < banks.Length; i++)
        {
            _banks.Add(i, int.Parse(banks[i]));
        }
    }
}