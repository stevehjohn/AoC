using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2024._22;

public abstract class Base : Solution
{
    public override string Description => "Monkey market";

    protected static long SimulateRound(long number)
    {
        number ^= number << 6;

        number &= 0b0000_0000_1111_1111_1111_1111_1111_1111;

        number ^= number >> 5;
            
        number &= 0b0000_0000_1111_1111_1111_1111_1111_1111;

        number ^= number << 11;
            
        number &= 0b0000_0000_1111_1111_1111_1111_1111_1111;

        return number;
    }
}