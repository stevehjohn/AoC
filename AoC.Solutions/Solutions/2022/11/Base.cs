using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2022._11;

public abstract class Base : Solution
{
    public override string Description => "Monkey in the middle";

    private readonly List<Monkey> _monkeys = new();

    protected readonly List<int> Inspections = new();

    protected void InitialiseMonkeys()
    {
        for (var i = 1; i < Input.Length; i += 7)
        {
            var test = int.Parse(Input[i + 2][21..]);

            var pass = int.Parse(Input[i + 3][29..]);

            var fail = int.Parse(Input[i + 4][30..]);

            var @operator = Input[i + 1][23];

            var operand = Input[i + 1][25] == 'o' ? 0 : int.Parse(Input[i + 1][25..]);

            var monkey = new Monkey(test, pass, fail, @operator, operand);

            var items = Input[i][18..].Split(',', StringSplitOptions.TrimEntries);

            foreach (var item in items)
            {
                monkey.Items.Add(int.Parse(item));
            }

            _monkeys.Add(monkey);

            Inspections.Add(0);
        }

        //var m = 0;

        //foreach (var monkey in _monkeys)
        //{
        //    Console.WriteLine($"Monkey {m}:");
        //    Console.WriteLine($"  Starting items: {string.Join(", ", monkey.Items)}");
        //    Console.WriteLine($"  Operation: new = old {monkey.Operator} {(monkey.Operand == 0 ? "old" : monkey.Operand)}");
        //    Console.WriteLine($"  Test: divisible by {monkey.DivisorTest}");
        //    Console.WriteLine($"    If true: throw to monkey {monkey.PassTestMonkey}");
        //    Console.WriteLine($"    If false: throw to monkey {monkey.FailTestMonkey}");

        //    m++;
        //}
    }

    protected void PlayRounds(int rounds = 20)
    {
        for (var round = 0; round < rounds; round++)
        {
            var monkeyIndex = 0;

            foreach (var monkey in _monkeys)
            {
                while (monkey.Items.Count > 0)
                {
                    Inspections[monkeyIndex]++;

                    var item = monkey.Items[0];

                    monkey.Items.RemoveAt(0);

                    var operand = monkey.Operand == 0 ? item : monkey.Operand;

                    item = monkey.Operator == '*' ? item * operand : item + operand;

                    item = (int) Math.Floor(item / 3m);

                    if (item % monkey.DivisorTest == 0)
                    {
                        _monkeys[monkey.PassTestMonkey].Items.Add(item);
                    }
                    else
                    {
                        _monkeys[monkey.FailTestMonkey].Items.Add(item);
                    }
                }

                monkeyIndex++;
            }
        }
    }
}