using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2024._11;

public abstract class Base : Solution
{
    public override string Description => "Plutonian pebbles";

    protected readonly LinkedList<long> Stones = [];
    
    protected void ParseInput()
    {
        var stones = Input[0].Split(' ').Select(long.Parse).ToArray();

        var node = Stones.AddFirst(stones[0]);

        for (var i = 1; i < stones.Length; i++)
        {
            node = Stones.AddAfter(node, stones[i]);
        }
    }

    protected void Blink()
    {
        var stone = Stones.First;
        
        for (var index = 0; index < Stones.Count; index++)
        {
            // ReSharper disable once PossibleNullReferenceException
            var value = stone.Value;
            
            if (value == 0)
            {
                stone.Value = 1;

                stone = stone.Next;
                
                continue;
            }

            var digits = (int) Math.Log10(value) + 1;
            
            if (digits % 2 == 0)
            {
                var pow = 1;
        
                for (var i = 0; i < digits / 2; i++)
                {
                    pow *= 10;
                }
            
                stone.Value = value / pow;

                stone = Stones.AddAfter(stone, value % pow);
                
                continue;
            }

            stone = stone.Next;
        }
    }
}