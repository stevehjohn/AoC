using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2016._19;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var binary = Convert.ToString(int.Parse(Input[0]), 2);

        binary = $"{binary[1..]}{binary[0]}";

        return Convert.ToInt32(binary, 2).ToString();
    }
}