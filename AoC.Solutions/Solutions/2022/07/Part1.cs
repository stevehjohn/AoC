namespace AoC.Solutions.Solutions._2022._07;

public class Part1 : Base
{
    public override string GetAnswer()
    {
        ProcessCommands();

        var oneHundredK = DirectorySizes.Where(ds => ds.Value <= 100_000);

        return oneHundredK.Sum(ds => ds.Value).ToString();
    }
}