using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2017._05;

public abstract class Base : Solution
{
    public override string Description => "Memory trampoline";

    private int[] _jumps;

    protected int Run(bool isPart2 = false)
    {
        var steps = 0;

        var position = 0;

        while (position >= 0 && position < _jumps.Length)
        {
            var previousPosition = position;

            position += _jumps[position];

            if (isPart2)
            {
                _jumps[previousPosition] = _jumps[previousPosition] > 2 ? _jumps[previousPosition] - 1 : _jumps[previousPosition] + 1;
            }
            else
            {
                _jumps[previousPosition]++;
            }

            steps++;
        }

        return steps;
    }

    protected void ParseInput()
    {
        _jumps = new int[Input.Length];

        for (var i = 0; i < Input.Length; i++)
        {
            _jumps[i] = int.Parse(Input[i]);
        }
    }
}