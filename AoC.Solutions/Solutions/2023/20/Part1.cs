using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._20;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        (int Lows, int Highs) total = (0, 0);

        for (var i = 0; i < 1_000; i++)
        {
            var result = SendPulses();

            total.Lows += result.Lows;

            total.Highs += result.Highs;
        }

        return (total.Lows * total.Highs).ToString();
    }

    private (int Lows, int Highs) SendPulses()
    {
        int lows = 0, highs = 0;
        
        var queue = new Queue<(string Source, bool Pulse, string Target)>();

        queue.Enqueue(("button", false, "broadcaster"));

        while (queue.TryDequeue(out var pulse))
        {
            // Console.WriteLine($"{pulse.Source} -{(pulse.Pulse ? "high" : "low")}-> {pulse.Target}");
            
            if (pulse.Pulse)
            {
                highs++;
            }
            else
            {
                lows++;
            }

            if (! Modules.TryGetValue(pulse.Target, out var module))
            {
                continue;
            }

            switch (module.Type)
            {
                case Type.Broadcast:
                    module.State = pulse.Pulse;
                    break;

                case Type.FlipFlop:
                    if (! pulse.Pulse)
                    {
                        module.State = ! module.State;
                    }
                    break;

                case Type.Conjunction:
                    module.ReceivedPulses[pulse.Source] = pulse.Pulse;
                    break;
            }

            foreach (var target in module.Targets)
            {
                if (pulse.Pulse && module.Type == Type.FlipFlop)
                {
                    continue;
                }

                var output = false;
                
                switch (module.Type)
                {
                    case Type.Broadcast:
                        output = module.State;
                        break;

                    case Type.FlipFlop:
                        if (! pulse.Pulse)
                        {
                            output = module.State;
                        }
                        break;

                    case Type.Conjunction:
                        output = ! module.ReceivedPulses.All(r => r.Value);
                        break;
                }
                
                queue.Enqueue((pulse.Target, output, target));
            }
        }

        return (lows, highs);
    }
}