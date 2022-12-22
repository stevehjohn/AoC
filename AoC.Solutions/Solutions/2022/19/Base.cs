using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2022._19;

public abstract class Base : Solution
{
    public override string Description => "Not enough minerals";

    private readonly List<Blueprint> _blueprints = new();

    private readonly List<State> _buildOptions = new();

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

    protected List<(int Best, int Id)> Simulate(int minutes)
    {
        var result = new List<(int Best, int Id)>();

        foreach (var blueprint in _blueprints)
        {
            var best = ExecuteBlueprint(blueprint, minutes);

            result.Add((best, blueprint.Id));
        }

        return result;
    }

    private int ExecuteBlueprint(Blueprint blueprint, int minutes)
    {
        var start = new State(0, 0, 0, 0, 1, 0, 0, 0, 0);

        var queue = new PriorityQueue<State, int>();

        queue.Enqueue(start, 0);

        var max = 0;

        var maxTime = 0;

        while (queue.Count > 0)
        {
            var state = queue.Dequeue();

            // Seems like a hack-y optimisation, but it works...
            if (state.Geodes + 1 < max && state.ElapsedTime >= maxTime)
            {
                continue;
            }

            if (state.Geodes > max)
            {
                max = state.Geodes;

                maxTime = state.ElapsedTime;
            }

            GenerateBuildOptions(blueprint, state, minutes);

            foreach (var build in _buildOptions)
            {
                // Dunno why / 100 seems so helpful to execution time, but it is.
                if (build.Geodes + (minutes - build.ElapsedTime) / 100 >= max)
                {
                    queue.Enqueue(build, build.ElapsedTime);
                }
            }
        }

        return max;
    }

    private void GenerateBuildOptions(Blueprint blueprint, State state, int minutes)
    {
        _buildOptions.Clear();

        var build = new State(state);

        var delta = 0;

        // Geode Bot
        while (build.Ore < blueprint.GeodeCost.Ore || build.Obsidian < blueprint.GeodeCost.Obsidian)
        {
            GatherResources(build);

            if (build.ElapsedTime >= minutes)
            {
                break;
            }

            delta++;
        }

        if (build.Ore >= blueprint.GeodeCost.Ore && build.Obsidian >= blueprint.GeodeCost.Obsidian && build.ElapsedTime < minutes)
        {
            GatherResources(build);

            build.Ore -= blueprint.GeodeCost.Ore;

            build.Obsidian -= blueprint.GeodeCost.Obsidian;

            build.GeodeBots++;

            _buildOptions.Add(build);

            if (delta == 0)
            {
                return;
            }
        }

        delta = 0;

        // Obsidian Bot
        if (state.ObsidianBots < blueprint.MaxObsidianCost)
        {
            build = new State(state);

            while (build.Ore < blueprint.ObsidianCost.Ore || build.Clay < blueprint.ObsidianCost.Clay)
            {
                GatherResources(build);

                if (build.ElapsedTime >= minutes)
                {
                    break;
                }

                delta++;
            }

            if (build.Ore >= blueprint.ObsidianCost.Ore && build.Clay >= blueprint.ObsidianCost.Clay && build.ElapsedTime < minutes)
            {
                GatherResources(build);

                build.Ore -= blueprint.ObsidianCost.Ore;

                build.Clay -= blueprint.ObsidianCost.Clay;

                build.ObsidianBots++;

                _buildOptions.Add(build);

                if (delta == 0)
                {
                    return;
                }
            }
        }

        // Clay Bot
        if (state.ClayBots < blueprint.MaxClayCost)
        {
            build = new State(state);

            var cycles = (int) Math.Ceiling((blueprint.ClayCost.Ore - build.Ore) / (float) build.OreBots);

            if (cycles > 0)
            {
                GatherResources(build, cycles);
            }

            if (build.Ore >= blueprint.ClayCost.Ore && build.ElapsedTime < minutes)
            {
                GatherResources(build);

                build.Ore -= blueprint.ClayCost.Ore;

                build.ClayBots++;

                _buildOptions.Add(build);
            }
        }

        // Ore Bot
        if (state.OreBots < blueprint.MaxOreCost)
        {
            build = new State(state);

            var cycles = (int) Math.Ceiling((blueprint.OreCost.Ore - build.Ore) / (float) build.OreBots);

            if (cycles > 0)
            {
                GatherResources(build, cycles);
            }

            if (build.ElapsedTime < minutes)
            {
                GatherResources(build);

                build.Ore -= blueprint.OreCost.Ore;

                build.OreBots++;

                _buildOptions.Add(build);
            }
        }
    }

    private static void GatherResources(State state, int cycles = 1)
    {
        state.Ore += state.OreBots * cycles;

        state.Clay += state.ClayBots * cycles;

        state.Obsidian += state.ObsidianBots * cycles;

        state.Geodes += state.GeodeBots * cycles;

        state.ElapsedTime += cycles;
    }
}