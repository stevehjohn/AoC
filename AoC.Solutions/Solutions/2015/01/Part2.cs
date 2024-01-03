using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2015._01;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var floor = 0;

        int i;

        for (i = 0; i < Input[0].Length; i++)
        {
            floor += Input[0][i] == '(' ? 1 : -1;

            if (floor == -1)
            {
                break;
            }
        }

        return (i + 1).ToString();
    }
}