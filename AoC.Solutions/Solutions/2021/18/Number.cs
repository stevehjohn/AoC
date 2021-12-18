namespace AoC.Solutions.Solutions._2021._18;

public class Number
{
    public int? Value { get; set; }

    public Number Left { get; set; }

    public Number Right { get; set; }

    public static Number Parse(string input)
    {
        var openings = 0;

        var left = string.Empty;
        
        var right = string.Empty;

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
                left = input[1..i];

                right = input[(i + 1)..^1];
            }
        }

        return null;
    }
}