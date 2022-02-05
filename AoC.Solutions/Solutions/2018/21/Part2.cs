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

        cpu.Initialise();

        cpu.LoadProgram(Input);

        var previousValues = new HashSet<int>();

        var breakOn = Input.Single(i => i.StartsWith("eqrr"));

        var register = int.Parse(breakOn.Split(' ', StringSplitOptions.TrimEntries)[1]);

        cpu.Run(-1, "eqrr");

        var i = 0;

        while (true)
        {
            i++;

            cpu.Continue();

            var registerValue = cpu.GetRegisters()[register];

            if (previousValues.Contains(registerValue))
            {
                break;
            }

            previousValues.Add(registerValue);

            Console.WriteLine($"{i, 5}: {registerValue}");
        }
        
        return cpu.GetRegisters()[register].ToString();
    }
}