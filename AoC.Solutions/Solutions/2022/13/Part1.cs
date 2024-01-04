using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2022._13;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var packetPair = 0;

        var result = 0;

        for (var i = 0; i < Input.Length; i += 3)
        {
            packetPair++;

            if (Compare(Input[i].Replace("10", ":"), Input[i + 1].Replace("10", ":")) == -1)
            {
                result += packetPair;
            }
        }

        return result.ToString();
    }
}