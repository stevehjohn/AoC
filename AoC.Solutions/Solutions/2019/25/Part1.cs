using System.Text;
using AoC.Solutions.Solutions._2019.Computer;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._25;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var cpu = new Cpu();

        cpu.Initialise(65536);

        cpu.LoadProgram(Input);

        while (true)
        {
            Console.WriteLine(cpu.Run());

            Console.WriteLine(ReadString(cpu));

            var command = Console.ReadLine();

            WriteString(cpu, command);
        }

        return "TESTING";
    }

    private static string ReadString(Cpu cpu)
    {
        var result = new StringBuilder();

        while (cpu.UserOutput.Count > 0)
        {
            result.Append((char) cpu.UserOutput.Dequeue());
        }

        return result.ToString();
    }

    private static void WriteString(Cpu cpu, string input)
    {
        foreach (var c in input)
        {
            cpu.UserInput.Enqueue(c);
        }

        cpu.UserInput.Enqueue('\n');
    }
}