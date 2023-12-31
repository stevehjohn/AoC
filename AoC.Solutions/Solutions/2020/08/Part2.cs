using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2020._08;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        (int Accumulator, bool Terminated) result;

        string previousInstruction = null;

        var lineToModify = 0;

        while (true)
        {
            if (previousInstruction != null)
            {
                Input[lineToModify - 1] = previousInstruction;
            }

            previousInstruction = Input[lineToModify];

            if (Input[lineToModify][..3] == "nop" && int.Parse(Input[lineToModify][4..]) != 0)
            {
                Input[lineToModify] = $"jmp {Input[lineToModify][4..]}";
            }
            else
            {
                Input[lineToModify] = $"nop {Input[lineToModify][4..]}";
            }

            result = RunProgram();

            if (result.Terminated)
            {
                break;
            }

            lineToModify++;
        }

        return result.Accumulator.ToString();
    }
}