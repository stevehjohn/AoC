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
            cpu.Run();

            var output = ParseOutput(ReadString(cpu));

            Console.WriteLine(output.Room);

            output.Directions.ForEach(Console.WriteLine);

            output.Items.ForEach(Console.WriteLine);

            var command = Console.ReadLine();

            WriteString(cpu, command);
        }
    }

    private static (string Room, List<string> Directions, List<string> Items) ParseOutput(string output)
    {
        var lines = output.Split('\n');

        var room = string.Empty;

        var directions = new List<string>();

        var items = new List<string>();

        var mode = 0;

        foreach (var line in lines)
        {
            if (line.StartsWith("== "))
            {
                room = line;

                continue;
            }

            if (line == "Doors here lead:")
            {
                mode = 1;

                continue;
            }

            if (line == "Items here:")
            {
                mode = 2;

                continue;
            }

            if (mode > 0 && ! line.StartsWith("- "))
            {
                mode = 0;

                continue;
            }

            if (mode == 1)
            {
                directions.Add(line.Substring(2));

                continue;
            }

            if (mode == 2)
            {
                items.Add(line.Substring(2));
            }
        }

        return (room, directions, items);
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