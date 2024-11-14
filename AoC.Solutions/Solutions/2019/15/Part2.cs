using AoC.Solutions.Common;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._15;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        LoadMap();

        var result = SpreadGas();

        return result.ToString();
    }

    private void LoadMap()
    {
        var data = File.ReadAllLines(Part1ResultFile);

        var coords = data[0].Split(',', StringSplitOptions.TrimEntries);

        Origin = new Point(int.Parse(coords[0]), int.Parse(coords[1]));

        coords = data[1].Split(',', StringSplitOptions.TrimEntries);

        Destination = new Point(int.Parse(coords[0]), int.Parse(coords[1]));

        Map = new bool[data[2].Length, data.Length];

        for (var y = 2; y < data.Length; y++)
        {
            var line = data[y];

            for (var x = 0; x < line.Length; x++)
            {
                Map[x, y - 2] = line[x] == '1';
            }
        }

        Width = data[1].Length;

        Height = data.Length - 2;
    }

    private int SpreadGas()
    {
        var bots = new List<Bot>
                   {
                       new(new Point(Destination), null, Map)
                   };

        var max = 0;

        while (true)
        {
            var newBots = new List<Bot>();

            foreach (var bot in bots)
            {
                var moveResult = bot.Move();

                max = Math.Max(max, bot.Steps);

                if (moveResult == null)
                {
                    continue;
                }

                newBots.AddRange(moveResult);
            }

            if (newBots.Count == 0)
            {
                return max;
            }

            bots = newBots;
        }
    }
}