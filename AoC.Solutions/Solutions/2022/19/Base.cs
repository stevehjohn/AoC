using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2022._19;

public abstract class Base : Solution
{
    public override string Description => "Not enough minerals";

    private readonly List<Blueprint> _blueprints = new();

    protected void ParseInput()
    {
        foreach (var line in Input)
        {
            var parts = line.Split(':', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            var id = int.Parse(parts[0][10..]);

            var ingredients = parts[1].Split('.', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            var oreBot = new Materials(ingredients[0][21] - '0', 0, 0);

            var clayBot = new Materials(ingredients[1][22] - '0', 0, 0);

            var obsidianBot = new Materials(ingredients[2][26] - '0', int.Parse(ingredients[2][36..38].Trim()), 0);

            var geodeBot = new Materials(ingredients[3][23] - '0', 0, int.Parse(ingredients[3][33..35].Trim()));

            var blueprint = new Blueprint(id, oreBot, clayBot, obsidianBot, geodeBot);

            _blueprints.Add(blueprint);
        }
    }

    protected void Simulate(int minutes)
    {
        foreach (var blueprint in _blueprints)
        {
            Console.WriteLine($"BP Id: {blueprint.Id}");

            ExecuteBlueprint(blueprint, minutes);
        }
    }

    private static void ExecuteBlueprint(Blueprint blueprint, int minutes)
    {
        var max = 0;

        var queue = new TrimableQueue<State>();

        queue.Enqueue(new(0, 0, 0, 0, 1, 0, 0, 0, 0));

        var i = 1_000_000;

        while (queue.Count > 0)
        {
            var state = queue.Dequeue();

            i--;

            if (i == 0)
            {
                Console.Write(queue.Count);

                i = 1_000_000;

                var maxCaptured = max;

                Console.WriteLine($" {queue.Trim(s => s.Geodes < maxCaptured && s.ElapsedTime > state.ElapsedTime)}");
            }

            if (state.ElapsedTime == minutes)
            {
                if (state.Geodes > max)
                {
                    max = state.Geodes;

                    Console.WriteLine($"\nMAX: {max}\n");
                }
            }

            state.ElapsedTime++;

            var builds = GetPossibleBuilds(state, blueprint);

            state.Ore += state.OreBots;

            state.Clay += state.ClayBots;

            state.Obsidian += state.ObsidianBots;

            state.Geodes += state.GeodeBots;

            var newState = new State(state);

            queue.Enqueue(newState);

            foreach (var build in builds)
            {
                queue.Enqueue(build);
            }
        }
    }

    private static List<State> GetPossibleBuilds(State state, Blueprint blueprint)
    {
        var builds = new List<State>();

        State build;

        if (state.Ore >= blueprint.GeodeCost.Ore && state.Obsidian > blueprint.GeodeCost.Obsidian)
        {
            build = new State(state);

            build.Ore -= blueprint.GeodeCost.Ore;

            build.Obsidian -= blueprint.GeodeCost.Obsidian;

            build.GeodeBots++;

            builds.Add(build);

            return builds;
        }

        if (state.Ore >= blueprint.ObsidianCost.Ore && state.Clay > blueprint.ObsidianCost.Clay)
        {
            build = new State(state);

            build.Ore -= blueprint.ObsidianCost.Ore;

            build.Clay -= blueprint.ObsidianCost.Clay;

            build.ObsidianBots++;

            builds.Add(build);

            return builds;
        }

        if (state.Ore >= blueprint.OreCost.Ore)
        {
            build = new State(state);

            build.Ore -= blueprint.OreCost.Ore;

            build.OreBots++;

            builds.Add(build);

            return builds;
        }

        if (state.Ore >= blueprint.ClayCost.Ore)
        {
            build = new State(state);

            build.Ore -= blueprint.ClayCost.Ore;

            build.ClayBots++;

            builds.Add(build);
        }

        return builds;
    }
}