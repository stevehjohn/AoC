using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2025._03;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var joltage = 0L;
        
        foreach (var line in Input)
        {
            var result = new char[12];
            
            var startPosition = 0;

            for (var i = 0; i < 12; i++)
            {
                var required = 12 - i - 1;
                
                var searchEnd = line.Length - required;

                var highDigit = '0';
                
                var highPosition = startPosition;

                for (var j = startPosition; j < searchEnd; j++)
                {
                    if (line[j] > highDigit)
                    {
                        highDigit = line[j];
                        
                        highPosition = j;
                    }
                }

                result[i] = highDigit;
                
                startPosition = highPosition + 1;
            }

            var high = long.Parse(new string(result));
            
            joltage += high;
        }

        return joltage.ToString();
    }
}