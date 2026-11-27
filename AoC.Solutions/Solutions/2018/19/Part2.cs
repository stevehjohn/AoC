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

        cpu.SetRegisters([1, 0, 0, 0, 0, 0]);

        cpu.Run(32);

        var registers = cpu.GetRegisters();

        var result = SumFactors(registers[3] + registers[4]);

        return result.ToString();
    }

    private static long SumFactors(int value)
    {
        var sum = 0L;

        for (var i = 1; (long) i * i <= value; i++)
        {
            if (value % i != 0)
            {
                continue;
            }

            var other = value / i;

            sum += i;

            if (other != i)
            {
                sum += other;
            }
        }

        return sum;
    }
}