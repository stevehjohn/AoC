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
            var left = track[i];

            for (var j = i + 1; j < track.Length; j++)
            {
                var right = track[j];
                
                if (left.Position.ManhattanDistance(right.Position) == 2)
                {
                    var middle = new Point2D(left.Position);
                    
                    middle.StepTowards(right.Position);

                    if (Map[middle.X, middle.Y] == '.')
                    {
                        continue;
                    }

                    var saving = left.Steps - right.Steps;

                    if (! cheats.TryAdd(saving, 1))
                    {
                        cheats[saving]++;
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