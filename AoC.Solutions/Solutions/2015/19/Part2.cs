using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2015._19;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var reactions = Input.Length - 2;

        var molecule = Input[^1];

        var steps = 0;

        while (molecule != "e")
        {
            for (var i = 0; i < reactions; i++)
            {
                var parts = Input[i].Split(" => ", StringSplitOptions.TrimEntries);

                var replacement = parts[0];

                var target = parts[1];

                var index = molecule.LastIndexOf(parts[1], StringComparison.InvariantCulture);

                if (index > -1)
                {
                    molecule = $"{molecule[..index]}{replacement}{molecule[(index + target.Length)..]}";

                    steps++;
                }
            }
        }

        return steps.ToString();
    }
}