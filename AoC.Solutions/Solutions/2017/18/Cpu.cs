using System.Collections.Concurrent;

namespace AoC.Solutions.Solutions._2017._18;

public class Cpu
{
    private readonly Dictionary<char, long> _registers = new();

    public ConcurrentQueue<long> OutputQueue { get; } = new();

    public ConcurrentQueue<long> InputQueue { get; set; }

    private string[] _program;

    private long _programCounter;

    public long RunProgram(string[] program)
    {
        _program = program;

        return Continue();
    }

    public long Continue()
    {
        while (true)
        {
            var instruction = ParseLine(_program[_programCounter]);

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
                    if ((instruction.Register == '\0' ? instruction.Value : GetRegister(instruction.Register)) > 0)
                    {
                        _programCounter += instruction.SourceRegister == '\0' ? instruction.Value : GetRegister(instruction.SourceRegister);

                        continue;
                    }

                    break;
                case "snd":
                    OutputQueue.Enqueue(GetRegister(instruction.Register));

                    break;
                case "rcv":
                    if (InputQueue == null)
                    {
                        if (GetRegister(instruction.Register) != 0)
                        {
                            return OutputQueue.Last();
                        }
                    }
                    else
                    {
                        if (InputQueue.Count > 0)
                        {
                            if (InputQueue.TryDequeue(out var input))
                            {
                                SetRegister(instruction.Register, input);
                            }
                        }
                        else
                        {
                            return 0;
                        }
                    }

                    break;
            }

            _programCounter++;
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

    private static (string OpCode, char Register, int Literal, char SourceRegister, int Value) ParseLine(string line)
    {
        var parts = line.Split(' ', StringSplitOptions.TrimEntries);

        var register = '\0';

        var literal = 0;

        if (char.IsLetter(parts[1][0]))
        {
            register = parts[1][0];
        }
        else
        {
            literal = int.Parse(parts[1]);
        }

        var sourceRegister = '\0';

        var value = 0;

        if (parts.Length > 2)
        {
            if (char.IsLetter(parts[2][0]))
            {
                sourceRegister = parts[2][0];
            }
            else
            {
                value = int.Parse(parts[2]);
            }
        }

        return (parts[0], register, literal, sourceRegister, value);
    }
}