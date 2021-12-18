using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2021._18;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        //var left = Number.Parse(Input[0]);

        //for (var i = 1; i < Input.Length; i++)
        //{
        //    var right = Number.Parse(Input[i]);

        //    left = Add(left, right);

        //    Reduce(left);
        //}

        return "TESTING";
    }

    private static Number Add(Number left, Number right)
    {
        return new Number
               {
                   Left = left,
                   Right = right
               };
    }

    private static void Reduce(Number number)
    {
        var modified = true;

        while (modified)
        {
            modified = Explode(number);

            modified |= Split(number);
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
        }

        return Explode(number.Left, depth + 1) || Explode(number.Right, depth + 1);
    }

    private static Number FindAdjacent(Number number, int direction)
    {
        var probe = number.Parent;

        if (direction == -1)
        {
            while (probe != null && probe.Left == number)
            {
                var temp = probe;

                probe = probe.Parent;

                number = temp;
            }

            if (probe == null)
            {
                return null;
            }

            probe = probe.Left;

            while (probe.Value == null)
            {
                probe = probe.Right;
            }

            return probe;
        }

        if (direction == 1)
        {
            while (probe != null && probe.Right == number)
            {
                var temp = probe;

                probe = probe.Parent;

                number = temp;
            }

            if (probe == null)
            {
                return null;
            }

            probe = probe.Right;

            while (probe.Value == null)
            {
                probe = probe.Left;
            }

            return probe;
        }

        return null;
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
            number.Left = new Number { Value = (int) Math.Floor((decimal) number.Value / 2) };

            number.Right = new Number { Value = (int) Math.Ceiling((decimal) number.Value / 2) };

            number.Value = null;

            return true;
        }

        return false;
    }
}