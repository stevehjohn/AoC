using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2025._10;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var machines = MatrixSolver.ParseInput(Input).ToArray();

        var p2 = machines.Select(MatrixSolver.Solve).Sum();

        return p2.ToString();
    }
}