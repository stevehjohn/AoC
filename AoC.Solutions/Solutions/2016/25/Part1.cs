using AoC.Solutions.Solutions._2016.Common;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2016._25;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var initValue = 0;

        while (true)
        {
            if (Cpu.RunProgram(Input, new Dictionary<char, int> { { 'a', initValue } }) == 1)
            {
                break;
            }

            initValue++;
        }

        return initValue.ToString();
    }
}