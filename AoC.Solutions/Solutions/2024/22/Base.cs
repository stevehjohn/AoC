using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2024._22;

public abstract class Base : Solution
{
    public override string Description => "Monkey market";

    protected static long SimulateRound(long number)
    {
        number ^= number << 6;

        number %= 16_777_216;

        number ^= number >> 5;
            
        number %= 16_777_216;

        number ^= number << 11;
            
        number %= 16_777_216;

        return number;
    }
}