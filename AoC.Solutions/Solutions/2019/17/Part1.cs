using System.Text;
using AoC.Solutions.Extensions;
using AoC.Solutions.Solutions._2019.Computer;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._17;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var cpu = new Cpu();

        cpu.Initialise(65536);

        cpu.LoadProgram(Input);

        cpu.Run();

        var output = new StringBuilder();

        while (cpu.UserOutput.Count > 0)
        {
            output.Append((char) cpu.UserOutput.Dequeue());
        }

        var map = output.ToString().Trim().To2DArray();

        var alignments = 0;

        for (var y = 1; y < map.GetLength(1) - 1; y++)
        {
            for (var x = 1; x < map.GetLength(0) - 1; x++)
            {
                if (map[x, y] != '#')
                {
                    continue;
                }

                if (map[x - 1, y] == '#' && map[x, y - 1] == '#' && map[x + 1, y] == '#' && map[x, y + 1] == '#')
                {
                    alignments += x * y;
                }
            }
        }

        return alignments.ToString();
    }
}