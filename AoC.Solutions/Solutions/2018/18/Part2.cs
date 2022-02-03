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

        while (true)
        {
            cycle++;

            RunCycle();

            var stateHash = HashState();

            if (_states.ContainsKey(stateHash))
            {
                break;
            }

            _states.Add(stateHash, cycle);
        }

        var finalState = _states.Single(s => s.Value == 1000000000 % cycle).Key;

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