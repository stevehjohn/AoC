using AoC.Solutions.Solutions._2018.TimeMachine;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2018._19;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var cpu = new Cpu(6);

        cpu.LoadProgram(Input);

        cpu.SetRegisters(new[] { 1, 0, 0, 0, 0, 0 });

        cpu.Run(32);

        var registers = cpu.GetRegisters();

        var result = SumFactors(registers[3] + registers[4]);

        return result.ToString();
    }

    private static long SumFactors(int i)
    {
        var result = Enumerable.Range(1, i).Where(v => i % v == 0).Sum();

        return result;
    }
}