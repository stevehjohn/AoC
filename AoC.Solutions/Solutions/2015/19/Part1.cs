using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2015._19;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var reactions = Input.Length - 2;

        var molecule = Input[^1];

        var molecules = new HashSet<string>();

        for (var i = 0; i < reactions; i++)
        {
            var parts = Input[i].Split(" => ", StringSplitOptions.TrimEntries);

            var target = parts[0];

            var replacement = parts[1];

            var index = molecule.IndexOf(target, StringComparison.InvariantCulture);

            while (index > -1)
            {
                molecules.Add($"{molecule[..index]}{replacement}{molecule[(index + target.Length)..]}");

                index++;

                if (index >= molecule.Length)
                {
                    break;
                }

                index = molecule.IndexOf(target, index, StringComparison.InvariantCulture);
            }
        }

        return molecules.Count.ToString();
    }
}