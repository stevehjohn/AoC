using System.Text;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2018._18;

[UsedImplicitly]
public class Part2 : Base
{
    private readonly Dictionary<string, int> _states = new();

    public override string GetAnswer()
    {
        ParseInput();

        var i = 0;

        var lastStateIndex = 0;

        while (true)
        {
            i++;

            RunCycle();

            lastStateIndex = HashState(i);

            if (lastStateIndex > -1)
            {
                break;
            }
        }

        var remaining = 1_000_000_000 % lastStateIndex;

        for (i = 0; i < remaining - 1; i++)
        {
            RunCycle();
        }

        return GetResourceValue().ToString();
    }

    private int HashState(int index)
    {
        var builder = new StringBuilder();

        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                builder.Append(Map[x, y]);
            }
        }

        var state = builder.ToString();

        if (_states.ContainsKey(state))
        {
            return _states[state];
        }

        _states.Add(state, index);

        return -1;
    }
}