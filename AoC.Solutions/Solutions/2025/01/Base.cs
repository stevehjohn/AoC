using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2025._01;

public abstract class Base : Solution
{
    public override string Description => "Secret Entrance";

    protected int ProcessDocument()
    {
        var position = 50;

        var password = 0;

        foreach (var line in Input)
        {
            var clicks = int.Parse(line[1..]);

            var left = line[0] == 'L';

            password += RotateDial(ref position, left, clicks);
        }

        return password;
    }

    protected abstract int RotateDial(ref int position, bool left, int clicks);
}