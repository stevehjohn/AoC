using System.Text;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2024._17;

public abstract class Base : Solution
{
    public override string Description => "Chronospatial computer";

    private int[] _registers;

    private byte[] _program;

    protected string RunProgram()
    {
        var output = new StringBuilder();

        var counter = 0;

        while (counter < _program.Length)
        {
            var opcode = _program[counter];

            var operand = 0;

            switch (opcode)
            {
                case 0:
                case 2:
                case 5:
                    operand = _program[counter + 1];
                    
                    // 7???
                    
                    if (operand < 4)
                    {
                        break;
                    }

                    operand = _registers[operand - 3];

                    break;

                case 1:
                case 3:
                    operand = _program[counter + 1] & 0b0111;
                    break;
            }

            
            switch (opcode)
            {
                case 0:
                    _registers[0] /= (int) Math.Pow(2, operand);
                    break;
                    
                case 1:
                    _registers[1] ^= operand;
                    break;
                    
                case 2:
                    _registers[1] = operand % 8;
                    break;
                    
                case 3:
                    if (_registers[0] != 0)
                    {
                        counter = operand;
                    }
                    break;
                    
                case 4:
                    _registers[1] ^= _registers[2];
                    break;
                    
                case 5:
                    if (output.Length > 0 && output[^1] != ',')
                    {
                        output.Append(',');
                    }

                    output.Append(operand % 8);

                    break;
                    
                case 6:
                    _registers[1] = _registers[0] / (int) Math.Pow(2, operand);
                    break;
                    
                case 7:
                    _registers[2] = _registers[0] / (int) Math.Pow(2, operand);
                    break;
            }

            if (opcode != 3)
            {
                counter += 2;
            }
        }

        return output.ToString();
    }

    protected void ParseInput()
    {
        _registers = new int[3];

        for (var i = 0; i < 3; i++)
        {
            _registers[i] = int.Parse(Input[i][12..]);
        }

        _program = Input[4][9..].Split(',').Select(byte.Parse).ToArray();
    }
}