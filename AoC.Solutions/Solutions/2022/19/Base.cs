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

            break;
        }
    }

    private static void ExecuteBlueprint(Blueprint blueprint, int minutes)
    {
        var start = new State(0, 0, 0, 0, 1, 0, 0, 0, 0);

        var queue = new Queue<State>();

        queue.Enqueue(start);

        var max = 0;

        var i = 100_000;

        while (queue.Count > 0)
        {
            if (i == 0)
            {
                Console.WriteLine(queue.Count);

                i = 100_000;
            }

            i--;

            var state = queue.Dequeue();

            if (state.Geodes > max)
            {
                max = state.Geodes;

                Console.WriteLine(max);
            }

            if (state.ElapsedTime == minutes)
            {
                continue;
            }

            var builds = GetBuildOptions(blueprint, state);

            state.Ore += state.OreBots;

            state.Clay += state.ClayBots;

            state.Obsidian += state.ObsidianBots;

            state.Geodes += state.GeodeBots;

            var newState = new State(state);

            newState.ElapsedTime++;

            queue.Enqueue(newState);

            foreach (var build in builds)
            {
                build.ElapsedTime++;

                queue.Enqueue(build);
            }
        }
    }

    private static List<State> GetBuildOptions(Blueprint blueprint,  State state)
    {
        var options = new List<State>();

        State build;

        if (state.Ore >= blueprint.GeodeCost.Ore && state.Obsidian >= blueprint.GeodeCost.Obsidian)
        {
            build = new State(state);

            build.Ore -= blueprint.GeodeCost.Ore;

            build.Obsidian -= blueprint.GeodeCost.Obsidian;

            build.GeodeBots++;

            options.Add(build);
        }

        if (state.Ore >= blueprint.ObsidianCost.Ore && state.Clay >= blueprint.ObsidianCost.Clay && state.ObsidianBots < blueprint.MaxObsidianCost)
        {
            build = new State(state);

            build.Ore -= blueprint.ObsidianCost.Ore;

            build.Clay -= blueprint.ObsidianCost.Clay;

            build.ObsidianBots++;

            options.Add(build);
        }

        if (state.Ore >= blueprint.ClayCost.Ore && state.ClayBots < blueprint.MaxClayCost)
        {
            build = new State(state);

            build.Ore -= blueprint.ClayCost.Ore;

            build.ClayBots++;

            options.Add(build);
        }

        if (state.Ore >= blueprint.OreCost.Ore && state.OreBots < blueprint.MaxOreCost)
        {
            build = new State(state);

            build.Ore -= blueprint.OreCost.Ore;

            build.OreBots++;

            options.Add(build);
        }

        return options;
    }
}