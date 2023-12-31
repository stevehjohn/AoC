using AoC.Solutions.Infrastructure;
using AoC.Solutions.Solutions._2019.Computer;

namespace AoC.Solutions.Solutions._2019._07;

public abstract class Base : Solution
{
    public override string Description => "Amplifiers (CPU used unmodified)";

    public string GetAnswer(IEnumerable<int[]> phases)
    {
        var highestOutput = 0L;

        foreach (var phase in phases)
        {
            var amps = new Cpu[5];

            for (var i = 0; i < 5; i++)
            {
                amps[i] = new Cpu();

                amps[i].Initialise();

                amps[i].LoadProgram(Input);

                amps[i].UserInput.Enqueue(phase[i]);
            }
            
            var previousOutput = 0L;

            while (true)
            {
                var lastState = CpuState.AwaitingInput;

                for (var i = 0; i < 5; i++)
                {
                    amps[i].UserInput.Enqueue(previousOutput);

                    lastState = amps[i].Run();

                    previousOutput = amps[i].UserOutput.Dequeue();

                    if (lastState == CpuState.Halted && i == 4)
                    {
                        break;
                    }
                }

                if (lastState == CpuState.Halted)
                {
                    break;
                }
            }

            if (previousOutput > highestOutput)
            {
                highestOutput = previousOutput;
            }
        }

        return highestOutput.ToString();
    }
}