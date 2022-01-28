﻿using AoC.Solutions.Common;
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
            MoveCarts();

            Visualise();

            collisionPoint = CheckForCollision();

            if (collisionPoint != null)
            {
                break;
            }
        }

        Visualise(collisionPoint);

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