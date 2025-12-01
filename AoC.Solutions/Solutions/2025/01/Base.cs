using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2025._01;

public abstract class Base : Solution
{
    public override string Description => "Puzzle 01";

    private int _position = 50;

    protected int ProcessDocument(bool passThrough = false)
    {
        var password = 0;

        foreach (var line in Input)
        {
            var clicks = int.Parse(line[1..]);

            if (passThrough)
            {
                password += clicks / 100;
            }

            if (line[0] == 'L')
            {
                clicks = -clicks;
            }

            var oldPosition = _position;
            
            var newPosition = oldPosition + clicks;

            var crossings = Math.Abs((int) Math.Floor(newPosition / 100.0) - (int) Math.Floor(oldPosition / 100.0));

            _position = (newPosition % 100 + 100) % 100;

            if (passThrough)
            {
                password += crossings;
            }
            else if (_position == 0)
            {
                password++;
            }

        }

        return password;
    }
}