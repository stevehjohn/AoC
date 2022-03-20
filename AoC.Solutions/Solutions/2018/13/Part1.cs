using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2018._13;

[UsedImplicitly]
public class Part1 : Base
{
    public Part1()
    {
    }

    public Part1(IVisualiser<PuzzleState> visualiser) : base(visualiser)
    {
    }

    public override string GetAnswer()
    {
        ParseInput();

        Visualise();

        Point collisionPoint;

        while (true)
        {
            collisionPoint = MoveCarts();

            Visualise();

            if (collisionPoint != null)
            {
                break;
            }
        }

        Visualise(collisionPoint);

        EndVisualisation();

        return $"{collisionPoint.X},{collisionPoint.Y}";
    }
}