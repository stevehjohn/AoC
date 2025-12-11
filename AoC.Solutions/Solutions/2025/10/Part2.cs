using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2025._10;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var machines = ParseInput(Input).ToArray();

        var result = machines.Select(MatrixSolver.Solve).Sum();

        return result.ToString();
    }
    
    private static IEnumerable<Matrix> ParseInput(string[] input)
    {
        foreach (var line in input)
        {
            var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            var buttonTokens = parts[1..^1];

            var joltageToken = parts[^1];

            var buttons = buttonTokens
                .Select(t => t[1..^1]
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray())
                .ToArray();

            var joltages = joltageToken[1..^1]
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            yield return new Matrix(buttons, joltages);
        }
    }
}