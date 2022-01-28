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

        if (Visualiser != null)
        {
            Visualiser.PuzzleStateChanged(new PuzzleState { Map = Map, Carts = Carts.Select(c => new Cart(c)).ToList() });
        }

        Point collisionPoint;

        while (true)
        {
            MoveCarts();

            if (Visualiser != null)
            {
                Visualiser.PuzzleStateChanged(new PuzzleState { Map = Map, Carts = Carts.Select(c => new Cart(c)).ToList() });
            }

            collisionPoint = CheckForCollision();

            if (collisionPoint != null)
            {
                break;
            }
        }

        if (Visualiser != null)
        {
            Visualiser.PuzzleStateChanged(new PuzzleState { Map = Map, Carts = Carts.Select(c => new Cart(c)).ToList(), CollisionPoint = collisionPoint });
        }

        return $"{collisionPoint.X},{collisionPoint.Y}";
    }

    private Point CheckForCollision()
    {
        foreach (var cart in Carts)
        {
            foreach (var other in Carts)
            {
                if (cart == other)
                {
                    continue;
                }

                if (cart.Position.Equals(other.Position))
                {
                    return cart.Position;
                }
            }
        }

        return null;
    }
}