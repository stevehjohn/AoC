using AoC.Solutions.Solutions._2018.TimeMachine;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2018._21;

[UsedImplicitly]
public class Part2 : Base
{
    // ReSharper disable StringLiteralTypo
    public override string GetAnswer()
    {
        var cpu = new Cpu(6);

        cpu.LoadProgram(Input);

        var previousValues = new HashSet<int>();

        var breakOn = Input.Single(i => i.StartsWith("eqrr"));

        var register = int.Parse(breakOn.Split(' ', StringSplitOptions.TrimEntries)[1]);

        cpu.Run(-1, "eqrr");

        var last = 0;

        while (true)
        {
            cpu.Continue();

            var registerValue = cpu.GetRegister(register);

            if (! previousValues.Add(registerValue))
            {
                break;
            }

            last = registerValue;
        }

        return last.ToString();
    }
}