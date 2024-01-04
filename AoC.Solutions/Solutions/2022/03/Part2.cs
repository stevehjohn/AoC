using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2022._03;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var sum = 0;

        for (var i = 0; i < Input.Length; i += 3)
        {
            var common = Input[i].Where(item => Input[i + 1].Contains(item)).ToArray();

            common = common.Where(item => Input[i + 2].Contains(item)).ToArray();

            sum += common[0] & 0b0001_1111;

            if ((common[0] & 0b0010_0000) == 0)
            {
                sum += 26;
            }
        }

        return sum.ToString();
    }
}