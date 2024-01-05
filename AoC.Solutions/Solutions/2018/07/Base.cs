using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2018._07;

public abstract class Base : Solution
{
    public override string Description => "Ikea sleigh";

    protected readonly Dictionary<char, List<char>> Steps = new();

    protected IEnumerable<char> ParseInput()
    {
        var allSteps = new HashSet<char>();
        
        var allRequires = new HashSet<char>();

        foreach (var line in Input)
        {
            var step = line[36];

            var requires = line[5];

            if (! Steps.TryGetValue(step, out var requirements))
            {
                requirements = [];

                Steps.Add(step, requirements);
            }
            else
            {
                requirements = Steps[step];
            }

            requirements.Add(requires);

            allSteps.Add(step);

            allRequires.Add(requires);
        }

        return allRequires.Except(allSteps).OrderBy(s => s);
    }
}