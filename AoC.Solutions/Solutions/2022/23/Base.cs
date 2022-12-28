using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2022._23;

public abstract class Base : Solution
{
    public override string Description => "Unstable diffusion";

    protected const int SetMaxSize = 2_400;

    protected int StartEvaluation;

    protected void RotateEvaluations()
    {
        StartEvaluation++;

        if (StartEvaluation == 4)
        {
            StartEvaluation = 0;
        }
    }
}