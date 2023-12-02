using AoC.Solutions.Infrastructure;
using AoC.Solutions.Solutions._2019.Computer;
using System.Text;

namespace AoC.Solutions.Solutions._2019._21;

public abstract class Base : Solution
{
    public override string Description => "Jumping droids (CPU used unmodified)";

    protected string RunDroid(string commands)
    {
        var cpu = new Cpu();

        cpu.Initialise(65536);

        cpu.LoadProgram(Input);

        cpu.Run();

        foreach (var c in commands)
        {
            cpu.UserInput.Enqueue((byte) c);
        }

        cpu.Run();

        var output = new StringBuilder();

        while (cpu.UserOutput.Count > 0)
        {
            var value = cpu.UserOutput.Dequeue();

            if (value > 255)
            {
                return value.ToString();
            }

            output.Append((char) value);
        }

        Console.WriteLine(output.ToString());

        return "TESTING";
    }
}