namespace AoC.Solutions.Solutions._2022._11;

public class Part1 : Base
{
    public override string GetAnswer()
    {
        InitialiseMonkeys();

        PlayRounds();

        return GetMonkeyBusiness().ToString();
    }
}