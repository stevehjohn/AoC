using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2016._08;

public abstract class Base : Solution
{
    public override string Description => "MFA";

    protected readonly bool[,] Screen = new bool[50, 6];

    protected void Solve()
    {
        foreach (var line in Input)
        {
            ProcessLine(line);
        }
    }

    private void ProcessLine(string line)
    {
        if (line.StartsWith("rect"))
        {
            var coords = line[5..].Split('x').Select(int.Parse).ToArray();

            for (var y = 0; y < coords[1]; y++)
            {
                for (var x = 0; x < coords[0]; x++)
                {
                    Screen[x, y] = true;
                }
            }

            return;
        }

        int[] parameters;

        if (line.StartsWith("rotate column"))
        {
            line = line[16..];

            parameters = line.Split(" by ", StringSplitOptions.TrimEntries).Select(int.Parse).ToArray();

            for (var i = 0; i < parameters[1]; i++)
            {
                var temp = Screen[parameters[0], 5];

                for (var y = 5; y > 0; y--)
                {
                    Screen[parameters[0], y] = Screen[parameters[0], y - 1];
                }

                Screen[parameters[0], 0] = temp;
            }

            return;
        }

        line = line[13..];

        parameters = line.Split(" by ", StringSplitOptions.TrimEntries).Select(int.Parse).ToArray();

        for (var i = 0; i < parameters[1]; i++)
        {
            var temp = Screen[49, parameters[0]];

            for (var x = 49; x > 0; x--)
            {
                Screen[x, parameters[0]] = Screen[x - 1, parameters[0]];
            }

            Screen[0, parameters[0]] = temp;
        }
    }
}