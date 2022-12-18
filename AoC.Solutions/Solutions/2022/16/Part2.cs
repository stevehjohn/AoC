﻿using AoC.Solutions.Exceptions;

namespace AoC.Solutions.Solutions._2022._16;

public class Part2 : Base
{
    private const int StartMinutes = 26;

    private readonly HashSet<(Valve Valve, int Time, Valve ElephantValve, int OpenedValves)> _added = new();

    public override string GetAnswer()
    {
        ParseInput();

        OptimiseGraph();

        return Solve().ToString();
    }

    private int Solve()
    {
        var max = 0;

        var queue = new PriorityQueue<(Valve Valve, int Time, Valve ElephantValve, int ElephantTime, int ReleasedPressure, int OpenedValves, int AvailableTotalFlow), int>();

        var availableTotalFlow = Start.WorkingValves.Sum(v => v.Valve.FlowRate);

        queue.Enqueue((Start, StartMinutes, Start, StartMinutes, 0, 0, availableTotalFlow), 0);

        while (queue.Count > 0)
        {
            var node = queue.Dequeue();

            if (node.ReleasedPressure > max)
            {
                max = node.ReleasedPressure;
            }

            if (node.Time <= 0 && node.ElephantTime <= 0)
            {
                continue;
            }

            if (node.Valve.FlowRate > 0 && (node.OpenedValves & node.Valve.Designation) == 0 && node.Time > 0)
            {
                node.Time--;

                node.OpenedValves |= node.Valve.Designation;

                node.ReleasedPressure += node.Valve.FlowRate * node.Time;

                node.AvailableTotalFlow -= node.Valve.FlowRate;
            }

            if (node.ElephantValve.FlowRate > 0 && (node.OpenedValves & node.ElephantValve.Designation) == 0 && node.ElephantTime > 0)
            {
                node.ElephantTime--;

                node.OpenedValves |= node.ElephantValve.Designation;

                node.ReleasedPressure += node.ElephantValve.FlowRate * node.ElephantTime;

                node.AvailableTotalFlow -= node.Valve.FlowRate;
            }

            if (node.ReleasedPressure > max)
            {
                max = node.ReleasedPressure;
            }

            if (node.Time <= 0 && node.ElephantTime <= 0)
            {
                continue;
            }

            foreach (var elephantValve in node.ElephantValve.WorkingValves)
            {
                foreach (var valve in node.Valve.WorkingValves)
                {
                    if (node.ReleasedPressure + node.AvailableTotalFlow * node.ElephantTime * node.Time < max)
                    {
                        continue;
                    }

                    var isOpen = (node.OpenedValves & node.Valve.Designation) > 0;

                    var elephantOpen = (node.OpenedValves & elephantValve.Valve.Designation) > 0;

                    var extraPressure = (node.Time - valve.Cost) * valve.Valve.FlowRate * (isOpen ? 0 : 1);

                    extraPressure += (node.ElephantTime - elephantValve.Cost) * elephantValve.Valve.FlowRate * (elephantOpen ? 0 : 1);

                    var totalPressure = node.ReleasedPressure + extraPressure;

                    var priority = 10_000;

                    priority -= totalPressure;

                    priority += isOpen ? 20_000 : 0;

                    priority += elephantOpen ? 20_000 : 0;

                    var newItem = (valve.Valve, 60 - node.Time - node.ElephantTime, elephantValve.Valve, node.OpenedValves);

                    if (! _added.Contains(newItem))
                    {
                        queue.Enqueue((valve.Valve, node.Time - valve.Cost, elephantValve.Valve, node.ElephantTime - elephantValve.Cost, node.ReleasedPressure, node.OpenedValves, node.AvailableTotalFlow), -priority);

                        _added.Add(newItem);
                    }
                }
            }
        }

        if (max == 0)
        {
            throw new PuzzleException("Solution not found");
        }

        return max;
    }
}