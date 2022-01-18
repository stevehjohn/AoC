using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2020._08;

public abstract class Base : Solution
{
    public override string Description => "Console assembly debugging";

    protected (int Accumulator, bool Terminated) RunProgram()
    {
        var accumulator = 0;

        var programCounter = 0;

        var executed = new HashSet<int>();

        while (true)
        {
            if (executed.Contains(programCounter))
            {
                break;
            }

            var instruction = Input[programCounter];

            executed.Add(programCounter);

            switch (instruction[..3])
            {
                case "jmp":
                    programCounter += int.Parse(instruction[4..]);

                    break;

                case "acc":
                    accumulator += int.Parse(instruction[4..]);

                    programCounter++;

                    break;
                default:
                    programCounter++;

                    break;
            }

            if (programCounter >= Input.Length)
            {
                return (accumulator, true);
            }
        }

        return (accumulator, false);
    }
}