using AoC.Solutions.Common;
using JetBrains.Annotations;
using System.Text;

namespace AoC.Solutions.Solutions._2019._15;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        GetMap();

        var shortestPath = FindShortestRoute();

        SaveResult();


        return shortestPath.ToString();
    }

    private int FindShortestRoute()
    {
        var bots = new List<Bot>
                   {
                       new(new Point(Origin), new Point(Destination), Map)
                   };

        while (true)
        {
            var newBots = new List<Bot>();

            foreach (var bot in bots)
            {
                if (bot.IsHome)
                {
                    return bot.Steps;
                }

                newBots.AddRange(bot.Move());
            }

            bots = newBots;
        }
    }

    private void SaveResult()
    {
        var data = new StringBuilder();
        
        data.AppendLine($"{Origin.X},{Origin.Y}");

        data.AppendLine($"{Destination.X},{Destination.Y}");

        for (var y = 0; y < Width; y++)
        {
            for (var x = 0; x < Height; x++)
            {
                data.Append(Map[x, y] ? '1' : '0');
            }

            data.AppendLine();
        }

        File.WriteAllText(Part1ResultFile, data.ToString());
    }
}