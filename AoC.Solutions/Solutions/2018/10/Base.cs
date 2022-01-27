using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2018._10;

public abstract class Base : Solution
{
    public override string Description => "Writing in the stars";

    private (Point Position, Point Velocity)[] _stars;

    protected int Solve()
    {
        ParseInput();

        var cycles = 0;

        while (true)
        {
            cycles++;

            Cycle();

            if (SpellsWord())
            {
                break;
            }
        }

        return cycles;
    }

    protected bool SpellsWord()
    {
        var height = _stars.Max(s => s.Position.Y) - _stars.Min(s => s.Position.Y);

        return height < 10;
    }

    protected void Cycle()
    {
        foreach (var star in _stars)
        {
            star.Position.X += star.Velocity.X;

            star.Position.Y += star.Velocity.Y;
        }
    }

    protected void Dump()
    {
        Console.Clear();

        Console.CursorVisible = false;

        var xMin = _stars.Min(s => s.Position.X);

        var yMin = _stars.Min(s => s.Position.Y);

        Console.ForegroundColor = ConsoleColor.Yellow;

        foreach (var star in _stars)
        {
            Console.SetCursorPosition(star.Position.X - xMin + 1, star.Position.Y - yMin + 1);

            Console.Write('*');
        }

        Console.ForegroundColor = ConsoleColor.Green;

        Console.SetCursorPosition(0, 12);
    }

    protected void ParseInput()
    {
        _stars = new (Point Position, Point Velocity)[Input.Length];

        var i = 0;

        foreach (var line in Input)
        {
            _stars[i] = ParseLine(line);

            i++;
        }
    }

    private static (Point Position, Point Velocity) ParseLine(string line)
    {
        var split = line[10..^1].Split("> velocity=<", StringSplitOptions.TrimEntries);

        return (Point.Parse(split[0]), Point.Parse(split[1]));
    }
}