using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2022._04;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var overlaps = 0;

        foreach (var line in Input)
        {
            var (elf1, elf2) = ParseLine(line);

            if ((elf1.Left >= elf2.Left && elf1.Left <= elf2.Right)
                || (elf1.Right >= elf2.Left && elf1.Right <= elf2.Right)
                || (elf2.Right >= elf1.Left && elf2.Right <= elf1.Right)
                || (elf2.Right >= elf1.Left && elf2.Right <= elf1.Right))
            {
                overlaps++;
            }
        }

        return overlaps.ToString();
    }
}