using AoC.Solutions.Solutions._2018.TimeMachine;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2018._16;

[UsedImplicitly]
public class Part1 : Base
{
    private readonly Cpu _cpu = new(4);

    public override string GetAnswer()
    {
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

        for (var i = 0; i < OpCodes.Length; i++)
        {
            _cpu.SetRegisters(initial);

            _cpu.Execute(OpCodes[i], a, b, c);

            matches += expected.SequenceEqual(_cpu.GetRegisters()) ? 1 : 0;
        }

        return matches;
    }
}