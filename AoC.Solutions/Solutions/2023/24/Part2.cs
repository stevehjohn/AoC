using System.Numerics;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._24;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var parallel = GetParallelLines();

        var plane1 = Hail[parallel[0].FirstIndex];

        var plane2 = Hail[parallel[1].FirstIndex];

        var intersection = GetPlaneIntersection(plane1, plane2);
        
        return "Unknown";
    }
    
    private (Vector3 Vector, Vector3 Normal) GetPlaneIntersection((DoublePoint Position, DoublePoint Velocity) first, (DoublePoint Position, DoublePoint Velocity) second)
    {

        var p1 = new Vector3((float) first.Velocity.X, (float) first.Velocity.Y, (float) first.Velocity.Z);

        var p2 = new Vector3((float) second.Velocity.X, (float) second.Velocity.Y, (float) second.Velocity.Z);

        var p1Normal = Vector3.Normalize(p1);
        
        var p2Normal = Vector3.Normalize(p2);
        
        var p3Normal = Vector3.Cross(p1Normal, p2Normal);

        var determinant = p3Normal.LengthSquared();

        var vector = (Vector3.Cross(p3Normal, p2Normal) * p1.Length() + Vector3.Cross(p1Normal, p3Normal) * p2.Length()) / determinant;
        
        Console.WriteLine(vector);
        
        Console.WriteLine(p3Normal);

        return (vector, p3Normal);
    }

    private List<(int FirstIndex, int SecondIndex)> GetParallelLines()
    {
        var result = new List<(int FirstIndex, int SecondIndex)>();
        
        for (var left = 0; left < Hail.Count - 1; left++)
        {
            for (var right = left + 1; right < Hail.Count; right++)
            {
                if (CheckIfParallel(Hail[left], Hail[right]))
                {
                    result.Add((left, right));
                }
            }
        }

        return result;
    }

    private static bool CheckIfParallel((DoublePoint Position, DoublePoint Velocity) left, (DoublePoint Position, DoublePoint Velocity) right)
    {
        var a1 = left.Velocity.Y / left.Velocity.X;
        var b1 = left.Position.Y - a1 * left.Position.X;
        var a2 = right.Velocity.Y / right.Velocity.X;
        var b2 = right.Position.Y - a2 * right.Position.X;

        if (EqualsWithinTolerance(a1, a2))
        {
            return ! EqualsWithinTolerance(b1, b2);
        }

        return false;
    }
}