using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2024._11;

public abstract class Base : Solution
{
    public override string Description => "Plutonian pebbles";

    protected List<long> Stones = [];
    
    protected void ParseInput()
    {
        Stones = Input[0].Split(' ').Select(long.Parse).ToList();
    }

    protected void Blink()
    {
        for (var index = 0; index < Stones.Count; index++)
        {
            var stone = Stones[index];

            if (stone == 0)
            {
                Stones[index] = 1;
                
                continue;
            }

            var digits = (int) Math.Log10(stone) + 1;
            
            if (digits % 2 == 0)
            {
                var pow = 1;
        
                for (var i = 0; i < digits / 2; i++)
                {
                    pow *= 10;
                }
            
                Stones[index] = stone / pow;
                
                Stones.Insert(index + 1, stone % pow);
            }

            Stones[index] = stone * 2_024;
        }
    }
}