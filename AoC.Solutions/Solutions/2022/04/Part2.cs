namespace AoC.Solutions.Solutions._2022._04;

public class Part2 : Base
{
    public override string GetAnswer()
    {
        var overlaps = 0;

        foreach (var line in Input)
        {
            var pair = line.Split(',');

            var elf1 = new Range(pair[0]);
            
            var elf2 = new Range(pair[1]);

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