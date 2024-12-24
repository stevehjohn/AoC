using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._24;

[UsedImplicitly]
public class Part2 : Base
{
    private readonly Random _random = Random.Shared;
    
    public override string GetAnswer()
    {
        ParseInput();
        
        var incorrectBits = 0UL;

        for (var i = 0; i < 10; i++)
        {
            var x = GetRandomNumber();

            var y = GetRandomNumber();

            var expected = x + y;

            SetBusValue('x', x);

            SetBusValue('y', y);

            var actual = GetBusValue('z');

            Console.WriteLine(Convert.ToString((long) expected, 2).PadLeft(45, '0'));

            Console.WriteLine(Convert.ToString((long) actual, 2).PadLeft(45, '0'));
            
            Console.WriteLine(Convert.ToString((long) (actual ^ expected), 2).PadLeft(45, '0'));
            
            Console.WriteLine();
        }
        
        return Convert.ToString((long) incorrectBits, 2).PadLeft(45, '0');
    }

    private ulong GetRandomNumber()
    {
        return (ulong) (_random.NextInt64() & 0b1111_1111_1111_1111_1111_1111_1111_1111_1111_1111_1111);
    }
}