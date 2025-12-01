using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2025._01;

public abstract class Base : Solution
{
    public override string Description => "Puzzle 01";

    private int _position = 50;

    protected int ProcessDocument()
    {
        var password = 0;
        
        foreach (var line in Input)
        {
            var clicks = int.Parse(line[1..]);

            if (line[0] == 'L')
            {
                clicks = -clicks;
            }

            _position = (_position + clicks + 100) % 100;

            if (_position == 0)
            {
                password++;
            }
        }

        return password;
    }
}