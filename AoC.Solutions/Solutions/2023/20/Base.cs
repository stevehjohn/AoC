using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2023._20;

public abstract class Base : Solution
{
    public override string Description => "Pulse propagation";

    private readonly Dictionary<string, Module> _modules = new(); 
    
    protected (int Lows, int Highs) SendPulses(bool part2 = false)
    {
        int lows = 0, highs = 0;
        
        var queue = new Queue<(string Source, bool Pulse, string Target)>();

        queue.Enqueue(("button", false, "broadcaster"));

        while (queue.TryDequeue(out var pulse))
        {
            if (pulse.Pulse)
            {
                highs++;
            }
            else
            {
                lows++;
            }

            if (! _modules.TryGetValue(pulse.Target, out var module))
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
                
                if (part2)
                {
                    if (target == "rx" && ! output)
                    {
                        return (0, 0);
                    }
                }
                
                queue.Enqueue((pulse.Target, output, target));
            }
        }

        return (lows, highs);
    }
    
    protected void ParseInput()
    {
        foreach (var line in Input)
        {
            var parts = line.Split("->", StringSplitOptions.TrimEntries);

            Module module;

            string name;
            
            switch (parts[0][0])
            {
                case 'b':
                    module = new Module
                    {
                        Type = Type.Broadcast
                    };
                    name = parts[0];
                    
                    break;
                
                case '%':
                    module = new Module
                    {
                        Type = Type.FlipFlop
                    };
                    name = parts[0][1..];
                    
                    break;
                
                default:
                    module = new Module
                    {
                        Type = Type.Conjunction
                    };
                    name = parts[0][1..];
                    
                    break;
            }

            module.Targets = parts[1].Split(',', StringSplitOptions.TrimEntries).ToList();

            if (module.Type == Type.Conjunction)
            {
                module.ReceivedPulses = new Dictionary<string, bool>();
            }

            _modules.Add(name, module);
        }

        foreach (var module in _modules)
        {
            foreach (var target in module.Value.Targets)
            {
                if (_modules.TryGetValue(target, out var targetModule))
                {
                    if (targetModule.Type == Type.Conjunction)
                    {
                        targetModule.ReceivedPulses[module.Key] = false;
                    }
                }
            }
        }
    }
}