using AoC.Solutions.Solutions._2017.Common;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2017._18;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var cpu1 = new Cpu();

        cpu1.SetRegister('p', 0);

        var cpu2 = new Cpu();

        cpu2.SetRegister('p', 1);

        cpu1.InputQueue = cpu2.OutputQueue;

        cpu2.InputQueue = cpu1.OutputQueue;

        var cpu2Output = 0;

        cpu1.RunProgram(Input);

        cpu2.RunProgram(Input);

        cpu2Output += cpu2.OutputQueue.Count;

        while (true)
        {
            cpu1.Continue();

            cpu2.Continue();

            cpu2Output += cpu2.OutputQueue.Count;

            if (cpu1.InputQueue.IsEmpty && cpu2.InputQueue.IsEmpty)
            {
                break;
            }
        }

        return cpu2Output.ToString();
    }
}