﻿using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2018._04;

[UsedImplicitly]
public class Part1 : Base
{
    private List<(DateTime Time, int GuardId, Event Event)> _data;

    public override string GetAnswer()
    {
        _data = ParseInput();

        var guard = GetSleepiestGuard();

        var mostAsleep = GetMinuteMostAsleep(guard);

        return (guard * mostAsleep).ToString();
    }

    private int GetMinuteMostAsleep(int guardId)
    {
        var minutes = new int[60];

        var events = _data.Where(d => d.GuardId == guardId).ToList();

        var previousTime = DateTime.MaxValue;

        foreach (var @event in events)
        {
            if (@event.Event == Event.FallAsleep)
            {
                previousTime = @event.Time;

                continue;
            }

            if (@event.Event == Event.WakeUp)
            {
                while (previousTime < @event.Time)
                {
                    minutes[previousTime.Minute]++;

                    previousTime = previousTime.AddMinutes(1);
                }
            }
        }

        return minutes.Select((m, i) => (Minute: m, Index: i)).MaxBy(m => m.Minute).Index;
    }

    private int GetSleepiestGuard()
    {
        var sleepTimes = new Dictionary<int, TimeSpan>();

        var sleepTime = DateTime.MaxValue;

        foreach (var item in _data)
        {
            if (item.Event == Event.FallAsleep)
            {
                sleepTime = item.Time;

                continue;
            }

            if (item.Event == Event.WakeUp)
            {
                if (sleepTimes.ContainsKey(item.GuardId))
                {
                    sleepTimes[item.GuardId] += item.Time - sleepTime;
                }
                else
                {
                    sleepTimes.Add(item.GuardId, item.Time - sleepTime);
                }
            }
        }

        return sleepTimes.MaxBy(st => st.Value).Key;
    }

    private List<(DateTime Time, int GuardId, Event Event)> ParseInput()
    {
        var sortedInput = GetSortedInput();

        var result = new List<(DateTime Time, int GuardId, Event Event)>();

        var currentGuard = -1;

        foreach (var item in sortedInput)
        {
            var data = ParseLine(item, currentGuard);
            
            result.Add(data);

            currentGuard = data.GuardId;
        }

        return result;
    }

    private List<string> GetSortedInput()
    {
        var lines = new List<(DateTime Time, string Line)>();

        foreach (var line in Input)
        {
            var split = line.Split(']', StringSplitOptions.TrimEntries);

            lines.Add((DateTime.Parse(split[0][1..]), line));
        }

        return lines.OrderBy(l => l.Time).Select(l => l.Line).ToList();
    }

    private static (DateTime Time, int GuardId, Event Event) ParseLine(string line, int currentGuard)
    {
        var split = line.Split(']', StringSplitOptions.TrimEntries);

        var time = DateTime.Parse(split[0][1..]);

        var @event = Event.StartShift;

        switch (split[1][0])
        {
            case 'f':
                @event = Event.FallAsleep;

                break;
            case 'w':
                @event = Event.WakeUp;

                break;
            default:
                currentGuard = int.Parse(split[1].Replace("Guard #", string.Empty).Replace(" begins shift", string.Empty));

                break;
        }

        return (time, currentGuard, @event);
    }
}
