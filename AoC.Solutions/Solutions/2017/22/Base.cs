using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2017._22;

public abstract class Base : Solution
{
    public override string Description => "Sporifica virus";

    protected readonly HashSet<Point> Infected = new();

    protected Point Position;

    protected Point Direction = new(0, -1);

    protected void ParseInput()
    {
        var y = 0;

        foreach (var line in Input)
        {
            for (var x = 0; x < line.Length; x++)
            {
                if (line[x] == '#')
                {
                    Infected.Add(new Point(x, y));
                }
            }

            y++;
        }

        Position = new Point(Input[0].Length / 2, Input.Length / 2);
    }
}