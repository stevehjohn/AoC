using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2024._17;

public abstract class Base : Solution
{
    public override string Description => "Chronospatial computer";

    private long[] _registers;

    private byte[] _program;

    protected string RunProgram()
    {
        var output = new List<long>();

        for (var counter = 0; counter < _program.Length; counter += 2)
        {
            switch (_program[counter], _program[counter + 1])
            {
                case (0, var operand):
                    _registers[0] >>= (int) GetComboOperand(operand);
                    break;
                
                case (1, var operand):
                    _registers[1] ^= operand;
                    break;
                
                case (2, var operand):
                    _registers[1] = GetComboOperand(operand) % 8;
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
                
                case (5, var operand):
                    output.Add(GetComboOperand(operand) % 8);
                    break;
                
                case (6, var operand):
                    _registers[1] = _registers[0] >> (int) GetComboOperand(operand);
                    break;
                
                case (7, var operand):
                    _registers[2] = _registers[0] >> (int) GetComboOperand(operand);
                    break;
            }
        }

        return string.Join(',', output);
    }

    private long GetComboOperand(byte operand)
    {
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