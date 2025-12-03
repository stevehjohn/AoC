using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2025._03;

public abstract class Base : Solution
{
    public override string Description => "Lobby";

    protected long FindHighestJoltage(int requiredBatteries = 2)
    {
        var joltage = 0L;

        foreach (var line in Input)
        {
            joltage += FindHighestJoltage(requiredBatteries, line);
        }
        
        return joltage;
    }

    private static long FindHighestJoltage(int requiredBatteries, string batteryPack)
    {
        var result = new char[requiredBatteries];
            
        var startPosition = 0;

        for (var i = 0; i < requiredBatteries; i++)
        {
            var required = requiredBatteries - i - 1;
                
            var searchEnd = batteryPack.Length - required;

            var highDigit = '0';
                
            var highPosition = startPosition;

            for (var j = startPosition; j < searchEnd; j++)
            {
                if (batteryPack[j] > highDigit)
                {
                    highDigit = batteryPack[j];
                        
                    highPosition = j;
                }
            }

            result[i] = highDigit;
                
            startPosition = highPosition + 1;
        }

        return long.Parse(new string(result));
    }
}