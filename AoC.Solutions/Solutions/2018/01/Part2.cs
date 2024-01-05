using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2018._01;

[UsedImplicitly]
public class Part2 : Base
{
    private readonly HashSet<int> _found = [];

    private int _frequency;

    public override string GetAnswer()
    {
        int result;

        while (true)
        {
            result = RunCycle();

            if (result != int.MaxValue)
            {
                break;
            }
        }

        return result.ToString();
    }

    private int RunCycle()
    {
        foreach (var line in Input)
        {
            var sign = line[0] == '-' ? -1 : 1;

            _frequency += sign * int.Parse(line[1..]);

            if (! _found.Add(_frequency))
            {
                return _frequency;
            }
        }

        return int.MaxValue;
    }
}