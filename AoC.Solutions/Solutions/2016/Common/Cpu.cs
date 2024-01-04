using System.Text;

namespace AoC.Solutions.Solutions._2016.Common;

public static class Cpu
{
    public static int RunProgram(string[] input, Dictionary<char, int> registers)
    {
        var programCounter = 0;

        var output = new StringBuilder();

        while (true)
        {
            if (programCounter >= input.Length)
            {
                break;
            }

            var line = input[programCounter];

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
                        if (char.IsLetter(parts[2][0]))
                        {
                            programCounter += GetRegisterValue(registers, parts[2][0]);
                        }
                        else
                        {
                            programCounter += int.Parse(parts[2]);
                        }

                        continue;
                    }

                    break;
                case "tgl":
                    if (char.IsLetter(parts[1][0]))
                    {
                        value = GetRegisterValue(registers, parts[1][0]);
                    }
                    else
                    {
                        value = int.Parse(parts[1]);
                    }

                    if (programCounter + value >= input.Length)
                    {
                        break;
                    }

                    var toToggle = input[programCounter + value][..3];

                    var toggled = string.Empty;

                    switch (toToggle)
                    {
                        case "inc":
                            toggled = "dec";

                            break;
                        case "dec":
                        case "tgl":
                            toggled = "inc";

                            break;
                        case "jnz":
                            toggled = "cpy";

                            break;
                        case "cpy":
                            toggled = "jnz";

                            break;
                    }

                    input[programCounter + value] = $"{toggled} {input[programCounter + value][4..]}";

                    break;
                case "mul":
                    SetRegisterValue(registers, parts[3][0], GetRegisterValue(registers, parts[1][0]) * GetRegisterValue(registers, parts[2][0]));

                    break;
                case "out": 
                    if (char.IsLetter(parts[1][0]))
                    {
                        value = GetRegisterValue(registers, parts[1][0]);
                    }
                    else
                    {
                        value = int.Parse(parts[1]);
                    }

                    output.Append(value);

                    if (output.Length == 1 && output[0] == '1')
                    {
                        return -1;
                    }

                    if (output.Length > 1 && output[^1] == output[^2])
                    {
                        return -1;
                    }

                    if (output.Length > 100)
                    {
                        return 1;
                    }

                    break;
            }

            programCounter++;
        }

        return registers['a'];
    }

    private static int GetRegisterValue(Dictionary<char, int> registers, char register)
    {
        if (! registers.TryAdd(register, 0))
        {
            return registers[register];
        }

        return 0;
    }

    private static void SetRegisterValue(Dictionary<char, int> registers, char register, int value)
    {
        if (! registers.TryAdd(register, value))
        {
            registers[register] = value;
        }
    }
}