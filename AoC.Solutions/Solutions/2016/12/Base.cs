using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2016._12;

public abstract class Base : Solution
{
    public override string Description => "Monorail";

    protected int RunProgram(int registerC = 0)
    {
        var registers = new Dictionary<char, int>();

        if (registerC > 0)
        {
            registers.Add('c', registerC);
        }

        var programCounter = 0;

        while (true)
        {
            if (programCounter >= Input.Length)
            {
                break;
            }

            var line = Input[programCounter];

            var parts = line.Split(' ', StringSplitOptions.TrimEntries);

            int value;

            switch (parts[0])
            {
                case "cpy":
                    if (char.IsLetter(parts[1][0]))
                    {
                        value = GetRegisterValue(registers, parts[1][0]);
                    }
                    else
                    {
                        value = int.Parse(parts[1]);
                    }

                    SetRegisterValue(registers, parts[2][0], value);

                    break;
                case "inc":
                    value = GetRegisterValue(registers, parts[1][0]);

                    registers[parts[1][0]] = value + 1;

                    break;
                case "dec":
                    value = GetRegisterValue(registers, parts[1][0]);

                    registers[parts[1][0]] = value - 1;

                    break;
                case "jnz":
                    if (char.IsLetter(parts[1][0]))
                    {
                        value = GetRegisterValue(registers, parts[1][0]);
                    }
                    else
                    {
                        value = int.Parse(parts[1]);
                    }

                    if (value != 0)
                    {
                        programCounter += int.Parse(parts[2]);

                        continue;
                    }

                    break;
            }

            programCounter++;
        }

        return registers['a'];
    }

    private static int GetRegisterValue(Dictionary<char, int> registers, char register)
    {
        if (registers.ContainsKey(register))
        {
            return registers[register];
        }

        registers.Add(register, 0);

        return 0;
    }

    private static void SetRegisterValue(Dictionary<char, int> registers, char register, int value)
    {
        if (registers.ContainsKey(register))
        {
            registers[register] = value;

            return;
        }

        registers.Add(register, value);
    }
}