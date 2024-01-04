using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2021._05;

public abstract class Base : Solution
{
    public override string Description => "Hydrothermal vents";

    protected string GetAnswer(bool includeDiagonal)
    {
        var lines = Input.Select(Line.Parse).ToList();

        var width = Math.Max(lines.Max(l => l.Start.X), lines.Max(l => l.End.X)) + 1;

        var height = Math.Max(lines.Max(l => l.Start.Y), lines.Max(l => l.End.Y)) + 1;

        var board = new int[width * height];

        foreach (var point in lines.Where(line => line.IsAxial || includeDiagonal).SelectMany(line => line.GetPoints()))
        {
            board[point.X + point.Y * width]++;
        }

        return board.Count(c => c > 1).ToString();
    }
}