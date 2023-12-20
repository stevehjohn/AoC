using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2023._20;

public abstract class Base : Solution
{
    public override string Description => "Pulse propagation";

    private Dictionary<string, Module> _modules = new(); 
    
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

            module.Destinations = parts[1].Split(',', StringSplitOptions.TrimEntries).ToList();
            
            _modules.Add(name, module);
        }
    }
}