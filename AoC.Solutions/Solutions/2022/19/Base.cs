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

    protected int Simulate(int minutes)
    {
        var sumQuality = 0;

        foreach (var blueprint in _blueprints)
        {
            Console.WriteLine($"BP Id: {blueprint.Id}");

            var quality = ExecuteBlueprint(blueprint, minutes) * blueprint.Id;

            sumQuality += quality;
        }

        return sumQuality;
    }

    private static int ExecuteBlueprint(Blueprint blueprint, int minutes)
    {
        var start = new State(0, 0, 0, 0, 1, 0, 0, 0, 0);

        var queue = new Queue<State>();

        queue.Enqueue(start);

        var max = 0;

        var maxTime = 0;

        while (queue.Count > 0)
        {
            var state = queue.Dequeue();

            // TODO: Good optimisation?
            if (state.Geodes < max && state.ElapsedTime > maxTime)
            {
                continue;
            }

            if (state.Geodes > max)
            {
                max = state.Geodes;

                maxTime = state.ElapsedTime;

                Console.WriteLine($"{max} @ {state.ElapsedTime}");
            }

            var builds = GetBuildOptions(blueprint, state, minutes);

            foreach (var build in builds)
            {
                queue.Enqueue(build);
            }
        }

        return max;
    }

    private static List<State> GetBuildOptions(Blueprint blueprint, State state, int minutes)
    {
        var options = new List<State>();

        var build = new State(state);

        // Geode Bot
        while (build.Ore <= blueprint.GeodeCost.Ore && build.Obsidian <= blueprint.GeodeCost.Obsidian)
        {
            GatherResources(build);

            if (build.ElapsedTime >= minutes)
            {
                break;
            }
        }

        if (build.Ore >= blueprint.GeodeCost.Ore && build.Obsidian >= blueprint.GeodeCost.Obsidian && build.ElapsedTime < minutes)
        {
            GatherResources(build);

            build.Ore -= blueprint.GeodeCost.Ore;

            build.Obsidian -= blueprint.GeodeCost.Obsidian;

            build.GeodeBots++;

            options.Add(build);

            // TODO: Is this a good optimisation?
            return options;
        }

        // Obsidian Bot
        if (state.ObsidianBots < blueprint.MaxObsidianCost)
        {
            build = new State(state);

            while (build.Ore <= blueprint.ObsidianCost.Ore && build.Clay <= blueprint.ObsidianCost.Clay)
            {
                GatherResources(build);

                if (build.ElapsedTime >= minutes)
                {
                    break;
                }
            }

            if (build.Ore >= blueprint.ObsidianCost.Ore && build.Clay >= blueprint.ObsidianCost.Clay && build.ElapsedTime < minutes)
            {
                GatherResources(build);

                build.Ore -= blueprint.ObsidianCost.Ore;

                build.Clay -= blueprint.ObsidianCost.Clay;

                build.ObsidianBots++;

                options.Add(build);
            }
        }

        // Clay Bot
        if (state.ClayBots < blueprint.MaxClayCost)
        {
            build = new State(state);

            while (build.Ore <= blueprint.ClayCost.Ore)
            {
                GatherResources(build);

                if (build.ElapsedTime >= minutes)
                {
                    break;
                }
            }

            if (build.Ore >= blueprint.ClayCost.Ore && build.ElapsedTime < minutes)
            {
                GatherResources(build);

                build.Ore -= blueprint.ClayCost.Ore;

                build.ClayBots++;

                options.Add(build);
            }
        }

        // Ore Bot
        if (state.OreBots < blueprint.MaxOreCost)
        {
            build = new State(state);

            while (build.Ore <= blueprint.OreCost.Ore)
            {
                GatherResources(build);

                if (build.ElapsedTime >= minutes)
                {
                    break;
                }
            }

            if (build.Ore >= blueprint.OreCost.Ore && build.ElapsedTime < minutes)
            {
                GatherResources(build);

                build.Ore -= blueprint.OreCost.Ore;

                build.OreBots++;

                options.Add(build);
            }
        }

        return options;
    }

    private static void GatherResources(State state)
    {
        state.Ore += state.OreBots;

        state.Clay += state.ClayBots;

        state.Obsidian += state.ObsidianBots;

        state.Geodes += state.GeodeBots;

        state.ElapsedTime++;
    }
}