namespace AoC.Solutions.Solutions._2021._18;

public class Number
{
    public int? Value { get; set; }

    public Number Left { get; set; }

    public Number Right { get; set; }

    public static Number Parse(string input)
    {
        if (input.Length == 1)
        {
            return new Number
                   {
                       Value = int.Parse(input)
                   };
        }

        var openings = 0;

        for (var i = 0; i < input.Length; i++)
        {
            if (input[i] == '[')
            {
                openings++;

                continue;
            }

            if (input[i] == ']')
            {
                openings--;

                continue;
            }

            if (input[i] == ',' && openings == 1)
            {
                var left = input[1..i];

                var right = input[(i + 1)..^1];

                return new Number
                       {
                           Left = Parse(left),
                           Right = Parse(right)
                       };
            }
        }

        return null;
    }
}