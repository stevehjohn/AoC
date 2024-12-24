using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2024._24;

public abstract class Base : Solution
{
    public override string Description => "Crossed wires";

    protected readonly Dictionary<string, Gate> Gates = [];
    
    protected readonly Dictionary<string, bool> Wires = [];

    protected int MaxZ;
    
    protected void ParseInput()
    {
        var i = 0;

        var line = Input[0];
        
        while (! string.IsNullOrWhiteSpace(line))
        {
            Wires.Add(line[..3], line[5] == '1');

            i++;

            line = Input[i];
        }

        i++;

        while (i < Input.Length)
        {
            line = Input[i];

            var parts = line.Split(' ');

            var gate = new Gate(parts[0], parts[2], Enum.Parse<Type>(parts[1]));
            
            Gates.Add(parts[4], gate);

            if (parts[4][0] == 'z')
            {
                MaxZ = Math.Max(int.Parse(parts[4][1..]), MaxZ);
            }

            i++;
        }
    }
}