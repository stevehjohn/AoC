using AoC.Solutions.Extensions;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2019._04;

public abstract class Base : Solution
{
    public override string Description => "Forgotten password";

    protected string GetAnswer(bool singlePairRequired)
    {
        var range = Input[0].Split('-');

        var low = int.FastParse(range[0]);
        
        var high = int.FastParse(range[1]);

        return Count(0, 1, 0, 0, false).ToString();

        int Count(int value, int digit, int length, int run, bool valid)
        {
            if (length == 6)
            {
                valid |= singlePairRequired ? run == 2 : run >= 2;

                return valid && value >= low && value <= high ? 1 : 0;
            }

            var result = 0;

            for (var d = digit; d <= 9; d++)
            {
                var same = d == digit;

                var matched = singlePairRequired ? run == 2 : run >= 2;

                result += Count(
                    value * 10 + d,
                    d,
                    length + 1,
                    same ? run + 1 : 1,
                    valid || ! same && matched);
            }

            return result;
        }
    }
}