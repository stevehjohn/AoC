using AoC.Solutions.Common;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._20;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var state = Race();

        var baseTime = state.Steps;
        
        Console.WriteLine(baseTime);

        var count = 0;

        var track = new State[state.Steps];

        var i = 0;
        
        while (state != null)
        {
            track[i] = state;
            
            state = state.Previous;

            i++;
        }

        var cheats = new Dictionary<int, int>();
        
        for (i = 0; i < track.Length - 1; i++)
        {
            for (var j = i + 1; j < track.Length; j++)
            {
                if (track[i].Position.ManhattanDistance(track[j].Position) == 2)
                {
                    var middle = new Point2D(track[i].Position);
                    
                    middle.StepTowards(track[j].Position);

                    if (Map[middle.X, middle.Y] == '.')
                    {
                        continue;
                    }

                    if (cheats.ContainsKey(track[i].Steps - track[j].Steps))
                    {
                        cheats[track[i].Steps - track[j].Steps]++;
                    }
                    else
                    {
                        cheats.Add(track[i].Steps - track[j].Steps, 1);
                    }

                    count++;
                }
            }
        }

        foreach (var cheat in cheats.OrderBy(c => c.Key))
        {
            Console.WriteLine($"Count: {cheat.Value}, saving: {cheat.Key}.");
        }

        return count.ToString();
    }
}