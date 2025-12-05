using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2025._05;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var index = ParseRanges();

        index += 2;

        var fresh = 0;
        
        for (; index < Input.Length; index++)
        {
            var ingredient = long.Parse(Input[index]);
            
            for (var i = 0; i < Ranges.Count; i++)
            {
                var range = Ranges[i];

                if (ingredient >= range.Start && ingredient <= range.End)
                {
                    fresh++;
                    
                    break;
                }
            }
        }

        return fresh.ToString();
    }
}