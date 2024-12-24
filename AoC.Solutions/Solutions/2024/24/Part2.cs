using System.Numerics;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._24;

[UsedImplicitly]
public class Part2 : Base
{
    private readonly Random _random = Random.Shared;
    
    public override string GetAnswer()
    {
        ParseInput();

        var wrong = Gates.Where(g => g.Key[0] == 'z' && g.Key != "z45" && g.Value.Type != Type.XOR).Select(g => g.Key).ToList();

        wrong = wrong.Union(Gates.Where(g => g.Key[0] != 'z'
                                             && g.Value.Left[0] is not ('x' or 'y')
                                             && g.Value.Right[0] is not ('x' or 'y')
                                             && g.Value.Type == Type.XOR).Select(g => g.Key)).ToList();
        
        for (var i = 0; i < 10; i++)
        {
            Console.WriteLine(TestCircuit());
        }

        return "Unknown";
    }

    private int TestCircuit()
    {
        var incorrectBits = 0UL;

        for (var i = 0; i < 100; i++)
        {
            var x = GetRandomNumber();

            var y = GetRandomNumber();

            var expected = x + y;

            SetBusValue('x', x);

            SetBusValue('y', y);

            var actual = GetOutputValue();

            incorrectBits |= actual ^ expected;
        }

        return BitOperations.PopCount(incorrectBits);
    }

    private ulong GetRandomNumber()
    {
        return (ulong) (_random.NextInt64() & 0b1111_1111_1111_1111_1111_1111_1111_1111_1111_1111_1111);
    }
}