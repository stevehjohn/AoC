using AoC.Solutions.Solutions._2019.Computer;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._13;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var cpu = new Cpu();

        cpu.Initialise(65536);

        cpu.LoadProgram(Input);

        cpu.Memory[0] = 2;

        while (true)
        {
            var paddle = 0;

            var ball = 0;

            var outputBuffer = new int[3];

            var result = cpu.Run();

            while (cpu.UserOutput.Count > 0)
            {
                outputBuffer[0] = (int) cpu.UserOutput.Dequeue();

                outputBuffer[1] = (int) cpu.UserOutput.Dequeue();

                outputBuffer[2] = (int) cpu.UserOutput.Dequeue();

                if (outputBuffer[0] == -1 && outputBuffer[1] == 0 && result == CpuState.Halted)
                {
                    return outputBuffer[2].ToString();
                }

                if (outputBuffer[2] == 3)
                {
                    paddle = outputBuffer[0];
                }

                if (outputBuffer[2] == 4)
                {
                    ball = outputBuffer[0];
                }
            }

            if (paddle < ball)
            {
                cpu.UserInput.Enqueue(1);
            }
            else if (paddle > ball)
            {
                cpu.UserInput.Enqueue(-1);
            }
            else
            {
                cpu.UserInput.Enqueue(0);
            }
        }
    }
}