using AoC.Solutions.Infrastructure;
using AoC.Solutions.Solutions._2019.Computer;

namespace AoC.Solutions.Solutions._2019._23;

public abstract class Base : Solution
{
    public override string Description => "IntCode network (CPU used unmodified)";

    protected long RunNetwork()
    {
        var cpus = new Dictionary<int, Cpu>();

        for (var i = 0; i < 50; i++)
        {
            var cpu = new Cpu();

            cpu.Initialise(65536);

            cpu.LoadProgram(Input);

            cpu.UserInput.Enqueue(i);
            
            cpus.Add(i, cpu);
        }

        while (true)
        {
            var allPackets = new Dictionary<int, List<(long X, long Y)>>();

            for (var i = 0; i < 50; i++)
            {
                var cpu = cpus[i];

                cpu.Run();

                while (cpu.UserOutput.Count > 0)
                {
                    var address = (int) cpu.UserOutput.Dequeue();
                    
                    var x = cpu.UserOutput.Dequeue();
                    
                    var y = cpu.UserOutput.Dequeue();

                    if (address == 255)
                    {
                        return y;
                    }

                    List<(long X, long Y)> packets;

                    if (allPackets.ContainsKey(address))
                    {
                        packets = allPackets[address];
                    }
                    else
                    {
                        packets = new List<(long X, long Y)>();

                        allPackets.Add(address, packets);
                    }

                    packets.Add((x, y));
                }
            }

            for (var i = 0; i < 50; i++)
            {
                var cpu = cpus[i];

                if (allPackets.TryGetValue(i, out var packets))
                {
                    foreach (var packet in packets)
                    {
                        cpu.UserInput.Enqueue(packet.X);

                        cpu.UserInput.Enqueue(packet.Y);
                    }

                    allPackets[i].Clear();
                }
                else
                {
                    cpu.UserInput.Enqueue(-1);
                }
            }
        }
    }
}