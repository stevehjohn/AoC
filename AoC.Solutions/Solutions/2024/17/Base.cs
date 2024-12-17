using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2024._17;

public abstract class Base : Solution
{
    public override string Description => "Chronospatial computer";

    private long[] _registers;

    protected long[] Program;

    protected List<long> RunProgram(long a = -1)
    {
        if (a > -1)
        {
            _registers[0] = a;
        }

        var output = new List<long>();

        for (var counter = 0L; counter < Program.Length; counter += 2)
        {
            if (Program[counter] is 0 or 2 or 5 or 6 or 7 && Program[counter + 1] == 7)
            {
                return null;
            }

            var combo = GetComboOperand(Program[counter + 1]);

            switch (Program[counter], Program[counter + 1])
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
        }

        return output;
    }

    private long GetComboOperand(long operand)
    {
        return operand switch
        {
            7 => long.MinValue,
            < 4 => operand,
            _ => _registers[operand - 4]
        };
    }

    protected void ParseInput()
    {
        _registers = new long[3];

        for (var i = 0; i < 3; i++)
        {
            _registers[i] = int.Parse(Input[i][12..]);
        }

        Program = Input[4][9..].Split(',').Select(long.Parse).ToArray();
    }
}