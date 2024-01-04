using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2018._10;

public abstract class Base : Solution
{
    public override string Description => "Writing in the stars";

    protected (Point Position, Point Velocity)[] Stars;

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

    private bool SpellsWord()
    {
        var height = Stars.Max(s => s.Position.Y) - Stars.Min(s => s.Position.Y);

        return height < 10;
    }

    private void Cycle()
    {
        foreach (var star in Stars)
        {
            star.Position.X += star.Velocity.X;

            star.Position.Y += star.Velocity.Y;
        }
    }

    private void ParseInput()
    {
        Stars = new (Point Position, Point Velocity)[Input.Length];

        var i = 0;

        foreach (var line in Input)
        {
            Stars[i] = ParseLine(line);

            i++;
        }
    }

    private static (Point Position, Point Velocity) ParseLine(string line)
    {
        var split = line[10..^1].Split("> velocity=<", StringSplitOptions.TrimEntries);

        return (Point.Parse(split[0]), Point.Parse(split[1]));
    }
}