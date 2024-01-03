namespace AoC.Solutions.Solutions._2022._11;

public class Part2 : Base
{
    public override string GetAnswer()
    {
        PlayRounds(10_000, false);

        return GetMonkeyBusiness().ToString();
    }
}