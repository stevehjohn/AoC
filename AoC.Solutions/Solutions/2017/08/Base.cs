using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2017._08;

public abstract class Base : Solution
{
    public override string Description => "I heard you like registers";

    protected int RunProgram(bool isPart2 = false)
    {
        var registers = new Dictionary<string, int>();

        var max = 0;

        foreach (var line in Input)
        {
            var operation = ParseLine(line);

            registers.TryGetValue(operation.ConditionRegister, out var value);

            var conditionMet = false;

            switch (operation.Operator)
            {
                case "<":
                    conditionMet = value < operation.Condition;

                    break;
                case ">":
                    conditionMet = value > operation.Condition;

                    break;
                case "<=":
                    conditionMet = value <= operation.Condition;

                    break;
                case ">=":
                    conditionMet = value >= operation.Condition;

                    break;
                case "==":
                    conditionMet = value == operation.Condition;

                    break;
                case "!=":
                    conditionMet = value != operation.Condition;

                    break;
            }

            if (conditionMet)
            {
                if (registers.ContainsKey(operation.TargetRegister))
                {
                    registers[operation.TargetRegister] += operation.Delta;
                }
                else
                {
                    registers.Add(operation.TargetRegister, operation.Delta);
                }
            }

            if (isPart2 && registers.Count > 0)
            {
                var currentMax = registers.Max(r => r.Value);

                if (currentMax > max)
                {
                    max = currentMax;
                }
            }
        }

        return isPart2 ? max : registers.Max(r => r.Value);
    }

    private static (string TargetRegister, int Delta, string ConditionRegister, string Operator, int Condition) ParseLine(string line)
    {
        var parts = line.Split(' ', StringSplitOptions.TrimEntries);

        var delta = int.Parse(parts[2]) * (parts[1] == "inc" ? 1 : -1);

        return (parts[0], delta, parts[4], parts[5], int.Parse(parts[6]));
    }
}