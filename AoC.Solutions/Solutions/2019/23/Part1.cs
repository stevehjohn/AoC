using AoC.Solutions.Solutions._2019.Computer;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._23;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
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
            var packets = new Dictionary<int, (int X, int Y)>();

            for (var i = 0; i < 50; i++)
            {
                var cpu = cpus[i];

                cpu.Run();

                while (cpu.UserOutput.Count > 0)
                {
                    var address = (int) cpu.UserOutput.Dequeue();
                    
                    var x = (int) cpu.UserOutput.Dequeue();
                    
                    var y = (int) cpu.UserOutput.Dequeue();

                    if (address == 255)
                    {
                        return y.ToString();
                    }

                    packets.Add(address, (x, y));
                }
            }

            for (var i = 0; i < 50; i++)
            {
                var cpu = cpus[i];

                if (packets.TryGetValue(i, out var packet))
                {
                    cpu.UserInput.Enqueue(packet.X);

                    cpu.UserInput.Enqueue(packet.Y);

                    packets.Remove(i);
                }
                else
                {
                    cpu.UserInput.Enqueue(-1);
                }
            }
        }
    }
}