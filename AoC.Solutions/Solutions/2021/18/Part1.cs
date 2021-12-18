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

    private void Reduce(Number number)
    {
        var modified = true;

        while (modified)
        {
            modified = Explode(number);

            modified |= Split(number);
        }
    }

    private bool Explode(Number number)
    {
        return false;
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

            number.Value = 0;

            return true;
        }

        return false;
    }
}