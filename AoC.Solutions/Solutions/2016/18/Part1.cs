using System.Text;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2016._18;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var result = Solve($"-{Input[0]}-");

        return result.ToString();
    }

    private static int Solve(string state)
    {
        var rows = new List<string> { state };

        for (var i = 0; i < 39; i++)
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