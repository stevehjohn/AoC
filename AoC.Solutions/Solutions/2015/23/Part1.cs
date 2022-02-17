using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2015._23;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var result = RunProgram();

        return result.ToString();
    }

    private int RunProgram()
    {
        var registers = new Dictionary<char, int> { { 'a', 0 }, { 'b', 0 } };

        var programCounter = 0;

        while (true)
        {
            if (programCounter >= Input.Length)
            {
                break;
            }

            var line = Input[programCounter];

            var parts = line.Split(new[] { ' ', ',' }, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            switch (parts[0])
            {
                case "hlf":
                    registers[parts[1][0]] /= 2;

                    break;
                case "tpl":
                    registers[parts[1][0]] *= 3;

                    break;
                case "inc":
                    registers[parts[1][0]]++;

                    break;
                case "jmp":
                    programCounter += int.Parse(parts[1]);

                    break;
                case "jie":
                    if (registers[parts[1][0]] % 2 == 0)
                    {
                        programCounter += int.Parse(parts[2]);

                        continue;
                    }

                    break;
                case "jio":
                    if (registers[parts[1][0]] == 1)
                    {
                        programCounter += int.Parse(parts[2]);

                        continue;
                    }

                    break;
            }

            programCounter++;
        }

        return registers['a'];
    }
}