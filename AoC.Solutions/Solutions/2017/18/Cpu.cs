namespace AoC.Solutions.Solutions._2017._18;

public class Cpu
{
    private readonly Dictionary<char, long> _registers = new();

    private readonly Queue<long> _outputQueue = new();

    public Queue<long> InputQueue { get; set; }

    public long RunProgram(string[] program)
    {
        var programCounter = 0;

        while (true)
        {
            var instruction = ParseLine(program[programCounter]);

            switch (instruction.OpCode)
            {
                case "set":
                    SetRegister(instruction.Register, instruction.SourceRegister == '\0' ? instruction.Value : GetRegister(instruction.SourceRegister));

                    break;
                case "add":
                    SetRegister(instruction.Register, GetRegister(instruction.Register) + (instruction.SourceRegister == '\0' ? instruction.Value : GetRegister(instruction.SourceRegister)));

                    break;
                case "mul":
                    SetRegister(instruction.Register, GetRegister(instruction.Register) * (instruction.SourceRegister == '\0' ? instruction.Value : GetRegister(instruction.SourceRegister)));

                    break;
                case "mod":
                    SetRegister(instruction.Register, GetRegister(instruction.Register) % (instruction.SourceRegister == '\0' ? instruction.Value : GetRegister(instruction.SourceRegister)));

                    break;
                case "jgz":
                    if (GetRegister(instruction.Register) > 0)
                    {
                        programCounter += (int) (instruction.SourceRegister == '\0' ? instruction.Value : GetRegister(instruction.SourceRegister));

                        continue;
                    }

                    break;
                case "snd":
                    _outputQueue.Enqueue(GetRegister(instruction.Register));

                    break;
                case "rcv":
                    if (GetRegister(instruction.Register) != 0)
                    {
                        if (InputQueue == null)
                        {
                            return _outputQueue.Last();
                        }
                    }

                    break;
            }

            programCounter++;
        }
    }

    public void SetRegister(char name, long value)
    {
        if (_registers.ContainsKey(name))
        {
            _registers[name] = value;
        }
        else
        {
            _registers.Add(name, value);
        }
    }

    private long GetRegister(char name)
    {
        if (_registers.ContainsKey(name))
        {
            return _registers[name];
        }

        _registers.Add(name, 0);

        return 0;
    }

    private static (string OpCode, char Register, char SourceRegister, int Value) ParseLine(string line)
    {
        var parts = line.Split(' ', StringSplitOptions.TrimEntries);

        var sourceRegister = '\0';

        var value = 0;

        if (parts.Length > 2)
        {
            if (! char.IsNumber(parts[2][0]) && parts[2][0] != '-')
            {
                sourceRegister = parts[2][0];
            }
            else
            {
                value = int.Parse(parts[2]);
            }
        }

        return (parts[0], parts[1][0], sourceRegister, value);
    }
}