using System.Text;
using AoC.Solutions.Infrastructure;
using AoC.Solutions.Solutions._2019.Computer;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2021._07;

[UsedImplicitly]
public class PartX : Solution
{
    public override string Description => "Easter egg (2019 CPU used)";

    public override string GetAnswer()
    {
        var cpu = new Cpu();

        cpu.Initialise();

        cpu.LoadProgram(Input);

        cpu.Run();

        var sb = new StringBuilder();

        foreach (var item in cpu.UserOutput)
        {
            sb.Append((char) item);
        }

        return sb.ToString().Trim();
    }
}