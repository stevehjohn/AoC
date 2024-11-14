using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2015._23;

public abstract class Base : Solution
{
    public override string Description => "Opening the Turing lock";

    protected int RunProgram(Dictionary<char, int> registers)
    {
        var programCounter = 0;

        while (true)
        {
            if (programCounter >= Input.Length)
            {
                break;
            }

            var line = Input[programCounter];

            var parts = line.Split([' ', ','], StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            switch (parts[0])
            {
                case "hlf":
                    registers[parts[1][0]] /= 2;

                    break;
                case "tpl":
                    registers[parts[1][0]] *= 3;

                    break;
                case "inc":
                    registers[parts[1][0]]++;

                    break;
                case "jmp":
                    programCounter += int.Parse(parts[1]);

                    continue;
                case "jie":
                    if (registers[parts[1][0]] % 2 == 0)
                    {
                        programCounter += int.Parse(parts[2]);

                        continue;
                    }

                    break;
                case "jio":
                    if (registers[parts[1][0]] == 1)
                    {
                        programCounter += int.Parse(parts[2]);

                        continue;
                    }

                    break;
            }

            programCounter++;
        }

        return registers['b'];
    }
}