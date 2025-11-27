// ReSharper disable DuplicatedSequentialIfBodies

using System.Text;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2016._18;

public abstract class Base : Solution
{
    public override string Description => "Like a rogue";

    protected static int Solve(string state, int totalRows)
    {
        state = $"-{state}-";

        var rows = new List<string> { state };

        for (var i = 0; i < totalRows - 1; i++)
        {
            var builder = new StringBuilder();

            for (var c = 0; c < state.Length - 2; c++)
            {
                if (state[c] == '^' && state[c + 1] == '^' && state[c + 2] != '^')
                {
                    builder.Append('^');

                    continue;
                }

                if (state[c] != '^' && state[c + 1] == '^' && state[c + 2] == '^')
                {
                    builder.Append('^');

                    continue;
                }
            
                if (state[c] == '^' && state[c + 1] != '^' && state[c + 2] != '^')
                {
                    builder.Append('^');

                    continue;
                }
            
                if (state[c] != '^' && state[c + 1] != '^' && state[c + 2] == '^')
                {
                    builder.Append('^');

                    continue;
                }

                builder.Append('.');
            }

            state = $"-{builder}-";

            rows.Add(state);
        }

        return rows.Select(r => r.Count(c => c == '.')).Sum();
    }
}