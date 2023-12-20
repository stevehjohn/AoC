using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2023._20;

public abstract class Base : Solution
{
    public override string Description => "Pulse propagation";

    protected readonly Dictionary<string, Module> Modules = new(); 
    
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

            Modules.Add(name, module);
        }

        foreach (var module in Modules)
        {
            foreach (var target in module.Value.Targets)
            {
                if (Modules.TryGetValue(target, out var targetModule))
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