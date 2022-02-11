using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2016._08;

[UsedImplicitly]
public class Part1 : Base
{
    private readonly bool[,] _screen = new bool[50, 6];

    public override string GetAnswer()
    {
        foreach (var line in Input)
        {
            ProcessLine(line);
        }

        var count = 0;

        for (var y = 0; y < 6; y++)
        {
            for (var x = 0; x < 50; x++)
            {
                count += _screen[x, y] ? 1 : 0;
            }
        }

        return count.ToString();
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
                    _screen[x, y] = true;
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
                var temp = _screen[parameters[0], 0];

                for (var y = 0; y < 5; y++)
                {
                    _screen[parameters[0], y] = _screen[parameters[0], y + 1];
                }

                _screen[parameters[0], 5] = temp;
            }

            return;
        }

        line = line[13..];

        parameters = line.Split(" by ", StringSplitOptions.TrimEntries).Select(int.Parse).ToArray();

        for (var i = 0; i < parameters[1]; i++)
        {
            var temp = _screen[0, parameters[0]];

            for (var x = 0; x < 49; x++)
            {
                _screen[x, parameters[0]] = _screen[x + 1, parameters[0]];
            }

            _screen[49, parameters[0]] = temp;
        }
    }
}