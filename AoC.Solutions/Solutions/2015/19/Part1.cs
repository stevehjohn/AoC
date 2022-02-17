using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2015._19;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var molecule = Input[44];

        var molecules = new HashSet<string>();

        for (var i = 0; i < 43; i++)
        {
            var parts = Input[i].Split(" => ", StringSplitOptions.TrimEntries);

            var target = parts[0];

            var replacement = parts[1];

            var index = molecule.IndexOf(target, StringComparison.InvariantCultureIgnoreCase);

            while (index > -1)
            {
                molecules.Add($"{molecule[..index]}{replacement}{molecule[(index + 1)..]}");

                index += replacement.Length + 1;

                if (index >= molecule.Length)
                {
                    break;
                }

                index = molecule.IndexOf(target, index, StringComparison.InvariantCultureIgnoreCase);
            }
        }

        return molecules.Count.ToString();;
    }
}