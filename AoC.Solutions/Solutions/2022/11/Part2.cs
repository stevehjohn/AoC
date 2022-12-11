namespace AoC.Solutions.Solutions._2022._11;

public class Part2 : Base
{
    public override string GetAnswer()
    {
        InitialiseMonkeys();

        PlayRounds(10_000, false);

        var mostActive = Inspections.OrderByDescending(i => i).Take(2).ToArray();

        return (mostActive[0] * mostActive[1]).ToString();
    }
}