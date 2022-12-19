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
            ExecuteBlueprint(blueprint, minutes);
        }
    }

    private static void ExecuteBlueprint(Blueprint blueprint, int minutes)
    {
        var max = 0;

        var queue = new PriorityQueue<State, int>();

        queue.Enqueue(new(0, 0, 0, 0, 1, 0, 0, 0, 0), 0);

        while (queue.Count > 0)
        {
            var state = queue.Dequeue();

            if (state.ElapsedTime == minutes)
            {
                Console.WriteLine($"{state}. END!\n");

                if (state.Geodes > max)
                {
                    max = state.Geodes;
                }

                continue;
            }

            state.Ore += state.OreBots;

            state.Clay += state.ClayBots;

            state.Obsidian += state.ObsidianBots;

            state.Geodes += state.GeodeBots;

            state.ElapsedTime++;

            var builds = GetPossibleBuilds(state, blueprint);

            if (builds.Count == 0)
            {
                queue.Enqueue(new State(state), CalculatePriority(state));
                
                Console.WriteLine($"{state}. Score: {CalculatePriority(state)}");

                continue;
            }

            if (builds.Count > 1)
            {
            }

            foreach (var build in builds)
            {
                queue.Enqueue(build, CalculatePriority(build));

                Console.WriteLine($"{build}. Score: {CalculatePriority(build)}");
            }

            Console.WriteLine();
        }

        Console.WriteLine(max);
    }

    private static int CalculatePriority(State state)
    {
        return 100_000_000 - (1_000_000 * state.GeodeBots + 10_000 * state.ObsidianBots + 100 * state.ClayBots + state.OreBots);
    }

    private static List<State> GetPossibleBuilds(State state, Blueprint blueprint)
    {
        var builds = new List<State>();

        State build;

        if (state.Ore > blueprint.OreCost.Ore)
        {
            build = new State(state);

            build.Ore -= blueprint.OreCost.Ore;

            build.OreBots++;

            builds.Add(build);
        }

        if (state.Ore > blueprint.ClayCost.Ore)
        {
            build = new State(state);

            build.Ore -= blueprint.ClayCost.Ore;

            build.ClayBots++;

            builds.Add(build);
        }

        if (state.Ore > blueprint.ObsidianCost.Ore && state.Clay > blueprint.ObsidianCost.Clay)
        {
            build = new State(state);

            build.Ore -= blueprint.ObsidianCost.Ore;

            build.Clay -= blueprint.ObsidianCost.Clay;

            build.ObsidianBots++;

            builds.Add(build);
        }

        if (state.Ore > blueprint.GeodeCost.Ore && state.Obsidian > blueprint.GeodeCost.Obsidian)
        {
            build = new State(state);

            build.Ore -= blueprint.ObsidianCost.Ore;

            build.Obsidian -= blueprint.ObsidianCost.Obsidian;

            build.GeodeBots++;

            builds.Add(build);
        }

        return builds;
    }
}