using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2018._13;

[UsedImplicitly]
public class Part2 : Base
{
    public Part2()
    {
    }

    public Part2(IVisualiser<PuzzleState> visualiser) : base(visualiser)
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
                if (Carts.Count == 1)
                {
                    break;
                }

                MoveCarts();

                Visualise(collisionPoint);
            }
        }

        Visualise(collisionPoint, true);

        EndVisualisation();

        return $"{Carts[0].Position.X},{Carts[0].Position.Y}";
    }
}