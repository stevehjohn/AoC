namespace AoC.Solutions.Solutions._2022._07;

public class Part2 : Base
{
    public override string GetAnswer()
    {
        ProcessCommands();

        const int size = 70_000_000;

        var used = DirectorySizes["/"];

        var delta = size - used;

        var matches = DirectorySizes.Where(ds => ds.Value + delta >= 30_000_000);

        return matches.Min(ds => ds.Value).ToString();
    }
}