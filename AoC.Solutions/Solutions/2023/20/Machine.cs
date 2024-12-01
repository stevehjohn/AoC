namespace AoC.Solutions.Solutions._2023._20;

public class Machine
{
    private Dictionary<string, Module> _modules; 
    
    public (int Lows, int Highs) SendPulses(string checkModule = null)
    {
        int lows = 0, highs = 0;
        
        var queue = new Queue<(string Source, bool Pulse, string Target)>();

        queue.Enqueue(("button", false, "broadcaster"));

        while (queue.TryDequeue(out var pulse))
        {
            if (checkModule != null)
            {
                if (! pulse.Pulse && pulse.Target == checkModule)
                {
                    return (0, 0);
                }
            }

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
                default:
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
                    default:
                        output = ! module.ReceivedPulses.All(r => r.Value);
                        break;
                }
                
                queue.Enqueue((pulse.Target, output, target));
            }
        }

        return (lows, highs);
    }
    
    public List<string> GetAllPenultimateConjunctions()
    {
        var result = new List<string>();
        
        foreach (var module in _modules.Where(m => m.Value.Targets.Contains("rx")))
        {
            foreach (var item in _modules)
            {
                if (item.Value.Targets.Contains(module.Key))
                {
                    result.Add(item.Key);
                }
            }
        }

        return result;
    }
    
    public void ParseInput(string[] input)
    {
        _modules = new Dictionary<string, Module>();
        
        foreach (var line in input)
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