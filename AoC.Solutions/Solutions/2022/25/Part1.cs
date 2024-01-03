namespace AoC.Solutions.Solutions._2022._25;

public class Part1 : Base
{
    private readonly Dictionary<char, int> _multipliers = new()
                                                          {
                                                              { '2', 2 },
                                                              { '1', 1 },
                                                              { '0', 0 },
                                                              { '-', -1 },
                                                              { '=', -2 }
                                                          };

    private readonly Dictionary<long, char> _digits = new()
                                                     {
                                                         { 2, '2' },
                                                         { 1, '1' },
                                                         { 0, '0' },
                                                         { -1, '-' },
                                                         { -2, '=' }
                                                     };

    public override string GetAnswer()
    {
        var sum = 0L;

        foreach (var line in Input)
        {
            sum += ParseSnafu(line);
        }

        return MakeSnafu(sum);
    }

    private string MakeSnafu(long number)
    {
        if (number == 0)
        {
            return string.Empty;
        }

        var remainder = number % 5;

        foreach (var digit in _digits)
        {
            if ((digit.Key + 5) % 5 == remainder)
            {
                var next = (number - digit.Key) / 5;

                return $"{MakeSnafu(next)}{digit.Value}";
            }
        }

        return string.Empty;
    }

    private long ParseSnafu(string snafu)
    {
        var value = 0L;

        var unit = 1L;

        for (var x = snafu.Length - 1; x >= 0; x--)
        {
            value += _multipliers[snafu[x]] * unit;

            unit *= 5;
        }

        return value;
    }
}