﻿using System.Runtime.CompilerServices;
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

    private static int ExecuteBlueprint(Blueprint blueprint, int minutes)
    {
        var start = new State(0, 0, 0, 0, 1, 0, 0, 0, 0);

        var queue = new PriorityQueue<State, int>();

        queue.Enqueue(start, 0);

        var max = 0;

        var maxTime = 0;

        while (queue.Count > 0)
        {
            var state = queue.Dequeue();

            if (state.Geodes < max && state.ElapsedTime > maxTime)
            {
                continue;
            }

            if (state.Geodes > max)
            {
                max = state.Geodes;

                maxTime = state.ElapsedTime;
            }

            var builds = GetBuildOptions(blueprint, state, minutes);

            foreach (var build in builds)
            {
                if (build.Geodes + (minutes - build.ElapsedTime) >= max)
                {
                    queue.Enqueue(build, build.ElapsedTime);
                }
            }
        }

        return max;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization)]
    private static List<State> GetBuildOptions(Blueprint blueprint, State state, int minutes)
    {
        var options = new List<State>();

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

            options.Add(build);

            if (delta == 0)
            {
                return options;
            }
        }

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

            while (build.Ore < blueprint.ClayCost.Ore)
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

            while (build.Ore < blueprint.OreCost.Ore)
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void GatherResources(State state)
    {
        state.Ore += state.OreBots;

        state.Clay += state.ClayBots;

        state.Obsidian += state.ObsidianBots;

        state.Geodes += state.GeodeBots;

        state.ElapsedTime++;
    }
}