using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2025._10;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var sum = 0;

        foreach (var line in Input)
        {
            var matrix = ParseLine(line);

            var solver = new MatrixSolver(matrix);

            sum += solver.Solve();
        }

        return sum.ToString();
    }

    private static Matrix ParseLine(string line)
    {
        var parts = line.Split(' ');

        var rows = new int[parts.Length - 2];

        for (var i = 1; i < parts.Length - 1; i++)
        {
            var digits = parts[i][1..^1].Split(',');

            var button = 0;

            for (var d = 0; d < digits.Length; d++)
            {
                button |= 1 << (digits[d][0] - '0');
            }

            rows[i - 1] = button;
        }

        var joltages = parts[^1][1..^1].Split(',');

        var totals = new int[joltages.Length];

        for (var i = 0; i < joltages.Length; i++)
        {
            totals[i] = int.Parse(joltages[i]);
        }

        return new Matrix(rows, totals);
    }
}