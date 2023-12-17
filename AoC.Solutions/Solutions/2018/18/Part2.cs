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

        var cycle = 0;

        int repeatIndex;

        while (true)
        {
            cycle++;

            RunCycle();

            var stateHash = HashState();

            if (! _states.TryAdd(stateHash, cycle))
            {
                repeatIndex = _states[stateHash];

                break;
            }
        }

        var delta = cycle - repeatIndex;

        var remainingCycles = 1_000_000_000 - repeatIndex;

        var finalState = _states.Single(s => s.Value == remainingCycles % delta + repeatIndex).Key;

        return GetResourceValue(finalState).ToString();
    }

    private static int GetResourceValue(string state)
    {
        var wood = 0;

        var yard = 0;

        for (var i = 0; i < state.Length; i++)
        {
            if (state[i] == '|')
            {
                wood++;

                continue;
            }

            if (state[i] == '#')
            {
                yard++;
            }
        }

        return wood * yard;
    }

    private string HashState()
    {
        var builder = new StringBuilder();

        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                builder.Append(Map[x, y]);
            }
        }

        return builder.ToString();
    }
}