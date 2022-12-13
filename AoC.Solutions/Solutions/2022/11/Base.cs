using System.Runtime.CompilerServices;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2022._11;

public abstract class Base : Solution
{
    public override string Description => "Monkey in the middle";

    private const int MonkeyCount = 8;

    private ulong _commonDivisor = 1;

    private readonly long[] _inspections = new long[MonkeyCount];

    [MethodImpl(MethodImplOptions.AggressiveOptimization)]
    protected void PlayRounds(int rounds = 20, bool reduceWorry = true)
    {
        var monkeys = InitialiseMonkeys();

        for (var round = 0; round < rounds; round++)
        {
            for (var i = 0; i < MonkeyCount; i++)
            {
                var monkey = monkeys[i];

                var items = monkey.Items;

                var count = items.Count;

                _inspections[i] += count;

                while (count-- > 0)
                {
                    var item = items.RemoveFirst();

                    var operand = monkey.Operand == 0 ? item : monkey.Operand;

                    item = monkey.Operator == '*' ? item * operand : item + operand;

                    if (reduceWorry)
                    {
                        item /= 3;
                    }
                    else
                    {
                        if (item >= _commonDivisor)
                        {
                            item %= _commonDivisor;
                        }
                    }

                    if (double.IsInteger((double) item / monkey.DivisorTest))
                    {
                        monkeys[monkey.PassTestMonkey].Items.Add(item);
                    }
                    else
                    {
                        monkeys[monkey.FailTestMonkey].Items.Add(item);
                    }
                }
            }
        }
    }

    protected long GetMonkeyBusiness()
    {
        var max1 = 0L;

        var max2 = 0L;

        for (var i = 0; i < MonkeyCount; i++)
        {
            var inspection = _inspections[i];

            if (inspection > max1)
            {
                max1 = inspection;
            }
        }

        for (var i = 0; i < MonkeyCount; i++)
        {
            var inspection = _inspections[i];

            if (inspection > max2 && inspection < max1)
            {
                max2 = inspection;
            }
        }

        return max1 * max2;
    }

    private Monkey[] InitialiseMonkeys()
    {
        var monkeys = new Monkey[MonkeyCount];

        var m = 0;

        for (var i = 1; i < Input.Length; i += 7)
        {
            var test = ulong.Parse(Input[i + 2][21..]);

            var pass = int.Parse(Input[i + 3][29..]);

            var fail = int.Parse(Input[i + 4][30..]);

            var @operator = Input[i + 1][23];

            var operand = Input[i + 1][25] == 'o' ? 0 : ulong.Parse(Input[i + 1][25..]);

            var monkey = new Monkey(test, pass, fail, @operator, operand);

            var items = Input[i][18..].Split(',', StringSplitOptions.TrimEntries);

            foreach (var item in items)
            {
                monkey.Items.Add(ulong.Parse(item));
            }

            monkeys[m] = monkey;

            _commonDivisor *= test;

            m++;
        }

        return monkeys;
    }
}