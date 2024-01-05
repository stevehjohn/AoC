namespace AoC.Solutions.Solutions._2021._18;

public class Number
{
    public int? Value { get; set; }

    public Number Left { get; set; }

    public Number Right { get; set; }

    public Number Parent { get; set; }

    public static Number Parse(string input, Number parent = null)
    {
        if (input.Length == 1)
        {
            return new Number
                   {
                       Parent = parent,
                       Value = int.Parse(input)
                   };
        }

        var openings = 0;

        for (var i = 0; i < input.Length; i++)
        {
            switch (input[i])
            {
                case '[':
                    openings++;

                    continue;
                case ']':
                    openings--;

                    continue;
                case ',' when openings == 1:
                {
                    var left = input[1..i];

                    var right = input[(i + 1)..^1];

                    var number = new Number
                    {
                        Parent = parent
                    };

                    number.Left = Parse(left, number);
                
                    number.Right = Parse(right, number);

                    return number;
                }
            }
        }

        return null;
    }

    public override string ToString()
    {
        return MakeString(this);
    }

    private static string MakeString(Number number)
    {
        if (number.Value != null)
        {
            return number.Value.ToString();
        }

        return $"[{MakeString(number.Left)},{MakeString(number.Right)}]";
    }
}