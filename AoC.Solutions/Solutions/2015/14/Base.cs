using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2015._14;

public abstract class Base : Solution
{
    public override string Description => "Reindeer olympics";

    protected readonly List<Reindeer> Reindeer = [];

    protected void ExecuteSecond()
    {
        foreach (var deer in Reindeer)
        {
            if (! deer.IsResting)
            {
                deer.Distance += deer.Speed;
            }

            deer.Ticks++;

            switch (deer.IsResting)
            {
                case true when deer.Ticks >= deer.RestTime:
                    deer.IsResting = false;

                    deer.Ticks = 0;
                
                    continue;
                case false when deer.Ticks >= deer.FlyTime:
                    deer.IsResting = true;

                    deer.Ticks = 0;
                    break;
            }
        }

        var furthest = Reindeer.Max(r => r.Distance);

        Reindeer.Where(r => r.Distance == furthest).ToList().ForEach(r => r.Points++);
    }

    protected void ParseInput()
    {
        foreach (var line in Input)
        {
            Reindeer.Add(ParseLine(line));
        }
    }

    private static Reindeer ParseLine(string line)
    {
        var parts = line.Split(' ');

        return new Reindeer(int.Parse(parts[3]), int.Parse(parts[6]), int.Parse(parts[13]));
    }
}