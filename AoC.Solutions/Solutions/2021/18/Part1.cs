using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2021._18;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var left = Number.Parse(Input[0]);

        for (var i = 1; i < Input.Length; i++)
        {
            var right = Number.Parse(Input[i]);

            left = Add(left, right);

            Reduce(left);
        }

        var magnitude = GetMagnitude(left);

        return magnitude.ToString();
    }

    private static Number Add(Number left, Number right)
    {
        var number = new Number
                     {
                         Left = left,
                         Right = right
                     };

        number.Left.Parent = number;

        number.Right.Parent = number;

        return number;
    }

    private static int GetMagnitude(Number number)
    {
        if (number.Value != null)
        {
            return (int) number.Value;
        }

        return GetMagnitude(number.Left) * 3 + GetMagnitude(number.Right) * 2;
    }

    private static void Reduce(Number number)
    {
        var modified = true;

        while (modified)
        {
            modified = Explode(number);

            if (modified)
            {
                continue;
            }

            modified = Split(number);
        }
    }

    private static bool Explode(Number number, int depth = 0)
    {
        if (number.Value != null)
        {
            return false;
        }

        if (depth == 4)
        {
            var left = FindAdjacent(number, -1);

            var right = FindAdjacent(number, 1);

            if (left != null)
            {
                left.Value += number.Left.Value;
            }

            if (right != null)
            {
                right.Value += number.Right.Value;
            }

            number.Left = null;

            number.Right = null;

            number.Value = 0;

            return true;
        }

        return Explode(number.Left, depth + 1) || Explode(number.Right, depth + 1);
    }

    private static Number FindAdjacent(Number number, int direction)
    {
        var probe = number.Parent;

        while (probe != null && (direction == -1 ? probe.Left : probe.Right) == number)
        {
            var temp = probe;

            probe = probe.Parent;

            number = temp;
        }

        if (probe == null)
        {
            return null;
        }

        probe = direction == -1 ? probe.Left : probe.Right;

        while (probe.Value == null)
        {
            probe = direction == -1 ? probe.Right : probe.Left;
        }

        return probe;
    }

    private static bool Split(Number number)
    {
        if (number == null)
        {
            return false;
        }

        if (number.Value == null)
        {
            return Split(number.Left) || Split(number.Right);
        }

        if (number.Value > 9)
        {
            number.Left = new Number
                          {
                              Value = (int) Math.Floor((decimal) number.Value / 2),
                              Parent = number
                          };

            number.Right = new Number
                           {
                               Value = (int) Math.Ceiling((decimal) number.Value / 2),
                               Parent = number
                           };

            number.Value = null;

            return true;
        }

        return false;
    }
}