using System.Text;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2016._16;

public abstract class Base : Solution
{
    public override string Description => "Dragon checksum";

    protected static string GetChecksum(string data)
    {
        var checksum = data;

        while (checksum.Length % 2 == 0)
        {
            var builder = new StringBuilder();

            for (var i = 0; i < checksum.Length; i += 2)
            {
                builder.Append(checksum[i] == checksum[i + 1] ? '1' : '0');
            }

            checksum = builder.ToString();
        }

        return checksum;
    }

    protected static string GenerateData(string state, int minLength)
    {
        var builder = new StringBuilder();

        builder.Append(state);

        while (builder.Length < minLength)
        {
            builder.Append('0');

            state = new string(state.Reverse().ToArray());

            state = state.Replace('0', 'x');
            state = state.Replace('1', '0');
            state = state.Replace('x', '1');

            builder.Append(state);

            state = builder.ToString();
        }

        return builder.ToString()[..minLength];
    }
}