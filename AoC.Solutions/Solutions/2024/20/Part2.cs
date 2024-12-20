using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._20;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var state = Race();

        var count = 0;

        var track = new State[state.Steps];

        var i = 0;
        
        while (state != null)
        {
            track[i] = state;
            
            state = state.Previous;

            i++;
        }

        for (i = 0; i < track.Length - 1; i++)
        {
            var left = track[i];

            for (var j = i + 1; j < track.Length; j++)
            {
                var right = track[j];
                
                if (left.Position.ManhattanDistance(right.Position) <= 20)
                {
                    var saving = left.Steps - right.Steps - 2;

                    if (saving <= 50)
                    {
                        continue;
                    }

                    count++;
                }
            }
        }

        return count.ToString();
    }
}