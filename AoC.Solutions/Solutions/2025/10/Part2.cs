using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2025._10;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var inputPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
            "Git", "AoC", "AoC.Solutions", "Solutions", "2025", "10", "input.clear");

        var machines = MatrixSolver.ParseInput(inputPath).ToArray();

        var p2 = machines.Select( MatrixSolver.SolvePart2).Sum();
        
        return p2.ToString();
    }
}