namespace AoC.Solutions.Solutions._2018._12;

public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        for (var g = 0; g < 20; g++)
        {
            RunGeneration();
        }

        return PotsWithPlants.Sum().ToString();
    }
}