using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2018._16;

[UsedImplicitly]
public class Part1 : Base
{
    // ReSharper disable StringLiteralTypo
    private readonly string[] _opCodes = { "addr", "addi", "mulr", "muli", "banr", "bani", "borr", "bori", "setr", "seti", "gtir", "gtri", "gtrr", "eqir", "eqri", "eqrr" };
    // ReSharper restore StringLiteralTypo
    
    private readonly Cpu _cpu = new();

    public override string GetAnswer()
    {
        _cpu.Initialise();

        var total = 0;

        for (var i = 0; i < Input.Length; i += 4)
        {
            if (string.IsNullOrWhiteSpace(Input[i]))
            {
                break;
            }

            var parts = Input[i + 1].Split(' ').Select(int.Parse).ToArray();

            var matches = CountMatchingCodes(parts[1], parts[2], parts[3], ParseRegisters(Input[i]), ParseRegisters(Input[i + 2]));

            if (matches >= 3)
            {
                total++;
            }
        }

        return total.ToString();
    }

    private int CountMatchingCodes(int a, int b, int c, int[] initial, int[] expected)
    {
        var matches = 0;

        for (var i = 0; i < _opCodes.Length; i++)
        {
            _cpu.SetRegisters(initial);

            _cpu.Execute(_opCodes[i], a, b, c);

            matches += expected.SequenceEqual(_cpu.GetRegisters()) ? 1 : 0;
        }

        return matches;
    }

    private static int[] ParseRegisters(string line)
    {
        line = line[9..][..^1];

        return line.Split(',', StringSplitOptions.TrimEntries).Select(int.Parse).ToArray();
    }
}