using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2015._20;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var input = int.Parse(Input[0]);

        var house = int.Parse(File.ReadAllText("2015.20.1.result")) + 1;

        while (Sigma(house) * 10 < input)
        {
            house++;
        }

        return house.ToString();
    }
}