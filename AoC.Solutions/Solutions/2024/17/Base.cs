using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2024._17;

public abstract class Base : Solution
{
    public override string Description => "Chronospatial computer";

    private long[] _registers;

    private byte[] _program;

    protected string RunProgram(long a = -1)
    {
        if (a > -1)
        {
            _registers[0] = a;
        }

        var output = new List<long>();

        for (var counter = 0; counter < _program.Length; counter += 2)
        {
            if (_program[counter] is 0 or 2 or 5 or 6 or 7 && _program[counter + 1] == 7)
            {
                return string.Empty;
            }

            var combo = GetComboOperand(_program[counter + 1]);

            switch (_program[counter], _program[counter + 1])
            {
                case (0, _):
                    _registers[0] >>= (int) combo;
                    break;
                
                case (1, var operand):
                    _registers[1] ^= operand;
                    break;
                
                case (2, _):
                    _registers[1] = combo % 8;
                    break;
                
                case (3, var operand):
                    if (_registers[0] != 0)
                    {
                        counter = operand - 2;
                    }
                    break;
                
                case (4, _):
                    _registers[1] ^= _registers[2];
                    break;
                
                case (5, _):
                    output.Add(combo % 8);
                    break;
                
                case (6, _):
                    _registers[1] = _registers[0] >> (int) combo;
                    break;
                
                case (7, _):
                    _registers[2] = _registers[0] >> (int) combo;
                    break;
            }

            if (a != -1 && output.Count > 0)
            {
                for (var i = 0; i < output.Count; i++)
                {
                    if (output[i] != _program[i])
                    {
                        return string.Empty;
                    }
                }
            }
        }

        if (a != -1 && output.Count != _program.Length)
        {
            return string.Empty;
        }

        return string.Join(',', output);
    }

    private long GetComboOperand(byte operand)
    {
        if (operand == 7)
        {
            return long.MinValue;
        }

        if (operand < 4)
        {
            return operand;
        }

        return _registers[operand - 4];
    }

    protected void ParseInput()
    {
        _registers = new long[3];

        for (var i = 0; i < 3; i++)
        {
            _registers[i] = int.Parse(Input[i][12..]);
        }

        _program = Input[4][9..].Split(',').Select(byte.Parse).ToArray();
    }
}