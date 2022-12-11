namespace AoC.Solutions.Solutions._2022._11;

public class Part1 : Base
{
    public override string GetAnswer()
    {
        InitialiseMonkeys();

        PlayRounds();

        var mostActive = Inspections.OrderByDescending(i => i).Take(2).ToArray();

        return (mostActive[0] * mostActive[1]).ToString();
    }
}