using AoC.Solutions.Common;
using AoC.Solutions.Exceptions;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2018._03;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var claims = new List<(int ClaimId, Point TopLeft, Point Size)>();

        foreach (var line in Input)
        {
            var data = ParseLine(line);

            claims.Add(data);

            FillArea(data.TopLeft, data.Size);
        }

        return FindNoOverlapClaim(claims).ToString();
    }

    private int FindNoOverlapClaim(List<(int ClaimId, Point TopLeft, Point Size)> claims)
    {
        foreach (var claim in claims)
        {
            if (! Overlaps(claim))
            {
                return claim.ClaimId;
            }
        }

        throw new PuzzleException("No solution found.");
    }

    private bool Overlaps((int ClaimId, Point TopLeft, Point Size) claim)
    {
        for (var y = 0; y < claim.Size.Y; y++)
        {
            for (var x = 0; x < claim.Size.X; x++)
            {
                if (Fabric[claim.TopLeft.X + x, claim.TopLeft.Y + y] == 'X')
                {
                    return true;
                }
            }
        }

        return false;
    }
}